var abm = {};

function JsonService_Inspeccionar(id_stat,id_data) {
	$.ajax({
		cache: false,
		url: "jsonservice/npmpicker.php?mode=SetInspeccion&id_data="+id_data,
		dataType: "json",
		success: function(data) {
			if(data.result=="done") {
				JsonService_loadFeeder(id_stat);
			}
		}
	});
}

function JsonService_Ajuste(id_stat,id_data) {
	$.ajax({
		cache: false,
		url: "jsonservice/npmpicker.php?mode=SetAjuste&id_data="+id_data,
		dataType: "json",
		success: function(data) {
			if(data.result=="done") {
				JsonService_loadFeeder(id_stat);
			}
		}
	});
}

function JsonService_DeleteStat(id_stat) {
	$.ajax({
		cache: false,
		url: "jsonservice/npmpicker.php?mode=DeleteStat&id_stat="+id_stat,
		dataType: "json",
		success: function(data) {
			if(data.result=="done") {
				$('#box_'+id_stat).remove();
			}
		}
	});
}

function JsonService_ResetJornada(id_linea,turno) {
	$.ajax({
		cache: false,
		url: "jsonservice/npmpicker.php?mode=ResetJornada&turno="+turno+"&id_linea="+id_linea,
		dataType: "json",
		success: function(data) {
			if(data.result=="done") {
				window.location.reload();
			}
		}
	});
}

function JsonService_loadFeeder(id_stat) {
	$.get('loadfeeder.php',{id_stat:id_stat},function(msg){
		$('#jsajax').html(msg);
	});
}

function dialogoReset(id_linea,turno) {
	bootbox.dialog({
	  message: "Realmente desea reiniciar las estadisticas de <b>SMD-"+id_linea+"</b> turno <b>"+turno+"</b>?",
	  title: "Reiniciar jornada",
	  buttons: {
		success: {
		  label: "Cancelar",
		  className: "btn-default"
		},
		danger: {
		  label: "Reiniciar jornada",
		  className: "btn-danger",
		  callback: function() {
			JsonService_ResetJornada(id_linea,turno);
		  }
		}
	  }
	});
}

function dialogoBorrar(feeder, id_stat) {
	bootbox.dialog({
	  message: "Realmente desea borrar los datos estadisticos de <b>"+feeder+"</b>?",
	  title: "Borrar datos",
	  buttons: {
		success: {
		  label: "Cancelar",
		  className: "btn-default"
		},
		danger: {
		  label: "Borrar",
		  className: "btn-danger",
		  callback: function() {
			JsonService_DeleteStat(id_stat);
		  }
		}
	  }
	});
}

var charts = {};
function loadFeeder(id_stat,lastid,feeder,stats,horas,avisos,picks,rate,real_error,status,rate_status) {		
	var alto = 200;
	var ancho = 430;

	var label_color = '#FFFFDB'; 

	var estado =  '';
	var line_color = '';
	
	switch(status) {
		case 'critico':
			estado = 'border: 4px solid #FF0000;';
			line_color = '#ff0000';
		break;
		case 'estable':
			line_color = '#2C9E18';
			estado = 'border: 4px solid #2C9E18;';
		break;
		case 'inestable':
		default:
			line_color = '#ff0000';
			estado =  'border:4px solid #8e8e8e;';
		break;
	}
	
	var rate_color;
	switch(rate_status) {
		case 'bajo': rate_color = 'default'; break;
		case 'medio': rate_color = 'warning'; break;
		case 'alto': rate_color = 'danger'; break;
	}
	
	var js_inspeccionar = "JsonService_Inspeccionar('"+id_stat+"','"+lastid+"');";
	var js_ajustar = "JsonService_Ajuste('"+id_stat+"','"+lastid+"');";
	
	var btn_inspec = ' <button type="button" class="btn btn-success btn-xs" onclick="'+js_inspeccionar+'">Solicitar inspeccion</button>';
	var btn_ajuste = ' <button type="button" class="btn btn-info  btn-xs" onclick="'+js_ajustar+'">Punto de ajuste</button>';
	 
	var btn_delete = ' <button type="button" class="btn btn-danger   btn-xs" onclick="dialogoBorrar(\''+feeder+'\',\''+id_stat+'\');" style="float:right;">x</button>';
	
	if(!abm.permiso_delete) { btn_delete = '';	}
	
	var drate = '<div class="btn-group"><button type="button" class="btn btn-default btn-sm active"><b>Errores: '+real_error+'</b></button><button type="button" class="btn btn-default btn-sm "><b>Pickups: '+picks+'</b></button><button type="button" class="btn btn-'+rate_color+' btn-sm active">Rate: '+rate+'%</button></div>';

	var char_top = '<div style="border-bottom: 1px solid #bcbcbc;background:#f9f9f9;">'+drate+'</div>';
	var char_bot = '<div style="border-top: 1px solid #bcbcbc;background:#f9f9f9;padding:3px;">'+btn_inspec + btn_ajuste+  btn_delete+ '</div>';

	var charinside = '<div style="margin:1px;'+estado+'">'+char_top+'<div id="'+id_stat+'" style="width:'+ancho+'px; height:'+alto+'px;" ></div>'+char_bot+'</div>';
	
	var el = $('#box_'+id_stat);
	
	if(el.length != 0) {
		el.html(charinside);
	} else {
		$('#stats').append('<div id="box_'+id_stat+'" style="float:left;">'+charinside+'</div>');
	}
	var chart;
	chart = new Highcharts.Chart({
		chart: {
			renderTo: id_stat,
			type: 'line',
			zoomType: 'x'
		},
		credits: {
		enabled: false
		},
		title: {
			text: feeder,
			margin:30,
			style: {			
				fontSize: '28px',			
				fontFamily: 'Verdana, sans-serif',
				color: 'black',	
			}
		},
		xAxis: {
			categories: horas
		},
		yAxis: {
			title: {
				text: 'Errores',
				style: {			
					fontSize: '16px',			
					fontFamily: 'Verdana, sans-serif',
					color: '#8e8e8e',	
				}
			},
			plotLines: [{
				value: 0,
				width: 1,
				color: '#808080'
			}]
		},
		 tooltip: {
			 formatter: function() {
				if(this.series.name != 'Errores'){
                      return '<small>'+this.x+'</small><br><b>Se dio aviso</b>';
				} else {		 
					 for(var i=0;i<this.series.data.length;i++){			
						var diferencia = "";
						if(this.point.x>0) {
							var calculo =  this.y - this.series.data[this.point.x - 1].y;
							if(calculo>0) {
								diferencia = '<br>Diferencia: <span style="color:#ff0000;">+' + calculo + '</span>';			
							}
						}
						if(this.point.config.marker) {
								return '<small>'+this.x+'</small> <br><b>PUNTO DE AJUSTE: '+ (this.y + diferencia)+'</b>';
						} else {
								return '<small>'+this.x+'</small> <br><b>Errores:<b> '+ (this.y + diferencia);
						}
					 }
				 }
			 }
		},
		legend: {
			enabled: false
		},				
		plotOptions: {			
			line: {
				enableMouseTracking: true,
				dataLabels: {
					enabled: true,
					formatter: function() { 
						var diferencia = "";
						if(this.point.x>0) {
							var calculo =  this.y - this.series.data[this.point.x - 1].y;
							if(calculo>0) {
								diferencia = '<span style="color:#ff0000;font-size:14px;">+' + calculo + '</span>';			
							}
						}
						return this.y + '<br>'+ diferencia; 
					},
                    borderRadius: 5,
                    backgroundColor: label_color,
                    borderWidth: 1,
                    borderColor: '#AAA',
                    y: -8,
					color: '#000',
					style: {
							fontSize: '16px',
							fontFamily: 'Verdana, sans-serif'
					}
				}
			},
			series: {
                marker: {
                    fillColor: '#FFFFFF',
                    lineWidth: 2,
					radius: 5,
                    lineColor: null 
                }
            },
			column: {
				dataLabels: {
					enabled: false
				}
			},
			bar: {
				dataLabels: {
					enabled: true
				},
				enableMouseTracking: true
			} 
		},
		series: [
		{
			name: 'Aviso',
            type: 'column',
			dashStyle: 'dotted',
			data: avisos,
			pointWidth: 1,
			borderWidth: 1,
            borderColor: '#28bc00',
			color: '#28bc00'
		},
		{
			name: 'Errores',
			data: stats,
			color: line_color
		}
		/*,
		{
			name: 'Reset',
            type: 'scatter',
			marker: { 
				fillColor: '#FF0000',
				lineWidth: null,
				radius: 10,
				symbol : "url(https://thewesternunion.custhelp.com/euf/rightnow/optimized/1385022320/themes/black/images/chat_disconnect.png)"
			},
			data: [null,null,null,null,
			{
				y: 9,
				marker: { 
					fillColor: '#FF0000',
					lineWidth: null,
					radius: 10,
					symbol : "url(https://thewesternunion.custhelp.com/euf/rightnow/optimized/1385022320/themes/black/images/chat_disconnect.png)"
				}
			},null,null,null,null],
            borderColor: '#000',
			color: '#000'
		}*/
		]
	});	
	
	var MAX = chart.xAxis[0].max;
	var MIN = chart.xAxis[0].max - 10;
	if(MIN<0) {MIN = 0;}
		
    chart.xAxis[0].setExtremes(MIN, MAX);
	chart.showResetZoom();
	
	charts[id_stat] = chart; 
}

function toggle(classe) {
	$('.'+classe).toggle();
}
