<?php
class Npmpicker {
	var $vars 	= array();
	
	var	$params = array(
		'turno' => 'T',
		'id_linea' => null,
		'id_stat' => null,
		'id_data' => null,
		'mode' => array(
			'GetFeedersInestables' => array('Obtiene estado de feeders de la linea (Define: id_linea, turno)'),
			'GetLineas' => array('Obtiene estado de lineas (Define: turno)'),
			'GetAll' => array('Obtiene estado de lineas e informacion de feeders (Define: id_linea, turno)'),
			'SetAjuste' => array('Establece un punto de ajuste (Define: id_data)'),
			'SetInspeccion' => array('Establece un punto de inspeccion (Define: id_stat)'),
			'ResetJornada' => array('Realiza un reinicio de datos en la linea y turno seleccionado (Define: id_linea, turno)'),
			'DeleteStat' => array('Elimina datos recolectados de un feeder (Define: id_stat)')
		) 
	);
	
	var	$params_info = array(
		'turno'=> 'Opciones: [T,M,N] -  Default:T ',
		'id_linea'=>'Filtrar por ID de linea <int> - Default: null'
	);
	
	function Params() {			
			return array_merge($this->params, $this->params_info);
	}	
	
	// GetLineas + GetFeedersInestables
	function GetAll($opt=array()) {
		$arr['lineas'] = $this->GetLineas($opt);
		$arr['feeders'] =  $this->GetFeedersInestables($opt);
		return $arr;
	}
		
	function GetFeedersInestables($opt=array()) {
		$this->vars = array_merge($this->params, $opt);
		
		$id_stat = $this->vars['id_stat'];
		
		$filtro = "";
		
		if(is_numeric($id_stat)) {
			$filtro = " id = '".$id_stat."' ";
		} else {
			$filtro = "
			id_linea = '".$this->vars['id_linea']."'
			AND 
				fecha = CURDATE()
			AND 
				turno = '".$this->vars['turno']."'
			";
		}
		
		$query = "
			SELECT 
			id,
			id_linea,
			maquina,
			modulo,
			tabla,
			feeder,
			turno,
			estado,
			fecha,
			hora,
			total_pickup,
			total_error
			

			FROM npmpicker.stat
			
			WHERE 
			".$filtro."
			
			AND
				estado = 'inestable'	
				
		";
		
		$arr = array();
		$sql = mysql_query($query);
		if(mysql_num_rows($sql)>0) {
			while($d = mysql_fetch_array($sql)) {	

				$info = array(			
					'id_stat' 		=> $d['id'],
					'maquina'		=> $d['maquina'],
					'modulo'		=> $d['modulo'],
					'tabla'			=> $d['tabla'],
					'feeder'		=> $d['feeder'],			
					'fecha'			=> $d['fecha'],			
					'hora'			=> $d['hora'],
					'format'		=> $d['maquina'].'-'.$d['modulo'].' T'.$d['tabla'].' '.$d['feeder'] 
				);

				$stat_info =  $this->GetFeederData($info);	
				$info['total_error'] = $stat_info['detail']['total_error'];
				$info['total_pickup'] = $stat_info['detail']['total_pickup'];
				$info['rate'] = $stat_info['detail']['rate'];

				$info['rate_status']  = 'bajo';
				if($info['rate']>2) {
					$info['rate_status'] = 'medio';
				}		
				if($info['rate']>5) {
					$info['rate_status'] = 'alto';
				}

				$info['last_id_data'] = $stat_info['last_id_data'];
				$info['status'] = $stat_info['detail']['status'];
				$info['reset'] = $stat_info['detail']['reset'];

				unset($stat_info['last_id_data']);
				unset($stat_info['detail']);
				
				$info['data'] = $stat_info;
				
				$arr['id_linea'][$d['id_linea']][] = $info;
			}
		}
		
		return $arr;
	}
	
	function GetLineas($opt=array()) {	
		$this->vars = array_merge($this->params, $opt);

		$query  = "
			select 
				s.id_linea,

				IF(
					(
						select p.ping from `npmpicker`.`ping` p 
						where 
						p.id_linea = s.id_linea limit 1 
					)
					>  
					DATE_SUB(NOW() , INTERVAL 15 MINUTE),true,false
				) as online

				from npmpicker.stat s group by s.id_linea
			";
		
		
 		$result = mysql_query($query);
								
		$arr = array();			
		if(mysql_num_rows($result) > 0){		

			$inestable = $this->GetLineasInestables();		
			$arr['turno'] = $this->vars['turno'];
			
			while($d = mysql_fetch_array($result))
			{
				$_id_linea = $d['id_linea'];
				$_online = $d['online'];
				$_count_inestable = $inestable[$_id_linea];
				
				if($_online) {
					$_online = true;
				} else {			
					$_online = false;
				}
				
				$arr['result'][$_id_linea] = array(
					'id_linea' => $_id_linea,
					'online' => $_online,
					'inestable' => $_count_inestable
				);
			}
		} else {
			$arr['error'] = 'Sin resultados';
		}
						
		return $arr;
	}

	function SetAjuste($opt=array()) {
		$this->vars = array_merge($this->params, $opt);

		$json = array();

		$id_data = $this->vars['id_data'];
		$query = "update npmpicker.data set ajuste = '1' where id = '".$id_data."' limit 1";

		if(is_numeric($id_data) && $id_data>0) {
			mysql_query($query);
			$json['result'] = 'done';
		} else {
			$json['result'] = 'error';	
		}
		return $json;
	}
	
	function SetInspeccion($opt=array()) {
		$this->vars = array_merge($this->params, $opt);

		$json = array();
		
		$id_data = $this->vars['id_data'];
		$query = "update npmpicker.data set inspeccion = '1' where id = '".$id_data."' limit 1";
		
		if(is_numeric($id_data) && $id_data>0) {
			mysql_query($query);
			$json['result'] = 'done';
		} else {
			$json['result'] = 'error';	
		}
		return $json;
	}
	
	function DeleteStat($opt=array()) {
		$this->vars = array_merge($this->params, $opt);

		$json = array();
		$query = "delete from npmpicker.stat where id = '".$this->vars['id_stat']."' limit 1";
		mysql_query($query);
		$json['result'] = 'done';
		return $json;
	}
	
	function ResetJornada($opt=array()) {
		$this->vars = array_merge($this->params, $opt);
			
		$linea = $this->vars['id_linea'];
		$turno = $this->vars['turno'];
		$json = array();
		
		if(is_numeric($linea) && $turno!="") {
			$query = "delete from npmpicker.stat where id_linea = '".$linea."' and turno = '".$turno."' and fecha = CURDATE() ";
			mysql_query($query);
			$json['result'] = 'done';
		} else {
			$json['result'] = 'error';
		}
		return $json;
	}
	
	// Private
	function GetLineasInestables() {
		$query = "
		SELECT 		
			COUNT(id_linea) as inestable,
			id_linea	

		FROM npmpicker.stat		
		WHERE 	
			fecha = CURDATE()		AND 		
			turno = '".$this->vars['turno']."'		AND	
			estado = 'inestable'

		group by id_linea
		";
		
		$result = mysql_query($query);
		$arr = array();			
		if(mysql_num_rows($result) > 0){				
			while($d = mysql_fetch_array($result))
			{
				$arr[$d['id_linea']] = $d['inestable'];
			}
		}else{
			$arr['error'] = 'Sin resultados';
		}
		return $arr;		
	}
	
	// Private
	function GetFeederData($info=array()) {
		$query = "
		SELECT	
			id,
			total_error, 
			total_pickup,
			DATE_FORMAT(hora, '%H:%i') as hora,
			inspeccion,
			ajuste,
			concat(round(( (total_error * 100) / total_pickup),2)) AS  rate
		FROM npmpicker.data 
		WHERE
			id_stat = '".$info['id_stat']."'
		ORDER BY id asc
		";
		
		$FEED = array();
		
		$maquina = $info['maquina'];
		$modulo = $info['modulo'];
		$feeder = $info['feeder'];
		$tabla = $info['tabla'];
		$last_id_data = null;
		
		$sql = mysql_query($query);
		if(mysql_num_rows($sql)>0) {
			while($d = mysql_fetch_array($sql)) {			
				$id = $d['id'];
				$error = $d['total_error'];
				$pickups = $d['total_pickup'];
				$hora = $d['hora'];
				$rate = $d['rate'];				
				$inspeccion = $d['inspeccion'];
				$ajuste = $d['ajuste'];
											
				$last_id_data = $id;

				$FEED[$hora] = array(
					'id'=>$id,
					'pickups'=>$pickups,
					'error'=>$error,
					'rate'=>$rate,
					'inspeccion'=>$inspeccion,
					'ajuste'=>$ajuste
				); 

	
				$existen_datas	= true;
			}
		}
		
		// Toda la DATA de pickups, inspecciones, ajustes y demas relacionada a sus respectivos ID_STAT se encuentra ahora en un array.
		// Ordeno DATAS por hora
		ksort($FEED);
		
		$return_info = array();
		$return_info['total_error'] = 0;
		$return_info['total_pickup'] = 0;
		
		// Defino algunos punteros.
		$sum_real_error_count = 0;
		$sum_real_pick_count = 0 ;

		$last_error_point = 0;
		$last_pick_point = 0;

		$resets = 0;
	
		$error_array = array();
		$horas_array = array();
		$inspeccion_array = array();	

		foreach($FEED as $hora => $feed_data) {
			// Recorro cada ID_STAT (cada feeder)
			$horas_array[] = "'".$hora."'";

			if($feed_data['ajuste']) {
				// Si existe un ajuste, 
				$error_array[] = array('error'=>$feed_data['error'],'ajuste'=>true);			
			} else {
				$error_array[] = $feed_data['error'];							
			}
			
			if($feed_data['inspeccion']) {
				// Si existe solictud de inspeccion
				$inspeccion_array[] = $feed_data['error'];	
			} else {
				$inspeccion_array[] = 'null';	
			}
			
			/*
			 * Si el error es menor al ultimo punto...
			 * Funcion que permite burlar el reset de una maquina y continuar la suma de errores.
			 */

			if($feed_data['error']<$last_error_point){
				$resets++;

				if($sum_real_error_count == 0) { 
					// Primer reset...
					$sum_real_error_count = $last_error_point;
					$sum_real_pick_count = $last_pick_point;									
				} else {						
					$sum_real_error_count += $last_error_point;
					$sum_real_pick_count += $last_pick_point;	
				}
			} 
			
			// Seteo el error actual.
			$last_error_point= $feed_data['error'];
			$last_pick_point = $feed_data['pickups'];

			$return_info['total_error']  = $sum_real_error_count;
			$return_info['total_pickup'] = $sum_real_pick_count;			
		} // END FOREACH
		
		$return_info['total_error']  = $sum_real_error_count + $last_error_point;
		$return_info['total_pickup']  = $sum_real_pick_count + $last_pick_point;
		$return_info['reset'] = $resets;	
		$return_info['rate'] =  $this->Porcentaje($return_info['total_error'],$return_info['total_pickup'],2);					
		
		$ultimos_errores = array_slice($error_array, -4, 4, true);
		$critico = 0;
		$critico_count = 1;
		$estable_count = 1;

		// Realizo un chequeo de los ultimos cuatro errores, para verificar la variacion. Si es mucha, el feeder se encuentra critico. 
		foreach($ultimos_errores as $lerr) {
			if(is_array($lerr)) {
				$lerr = $lerr['error'];
			}
			if($critico==0) {
				$critico = $lerr;
			} else {
				if($lerr > $critico) {	
					$critico_count++;
					$critico =  $lerr;	
					$estable_count = 1;						
				} elseif($lerr==$critico) {
					$estable_count++;
				}
			}
		}

		if($estable_count>=3) {
			$return_info['status'] = 'estable';	
		} else {
			$return_info['status'] = 'inestable';	
		}
		if($critico_count>=4) {
			$return_info['status'] = 'critico';	
		} else {
		}
		
		$FEED['detail'] = $return_info;
		$FEED['last_id_data'] = $last_id_data;
		
		return $FEED;
	}
	
	// Private
	function Porcentaje($val1, $val2, $precision) {
		$res = round( ($val1 / $val2) * 100, $precision );		
		return $res;
	}
}
?>