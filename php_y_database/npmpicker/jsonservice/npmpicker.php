<?php
header('Content-Type: application/json; charset=utf-8');
require('../../../lib/api.php');
require('class_Npmpicker.php');

// Conecto a DB
$db = new Newsan();
$db->conectar_sql();

$o = new Npmpicker();
$json = array();

switch($_GET['mode']) {
	case 'GetFeedersInestables':
		$json =  $o->GetFeedersInestables($_GET);
	break;
	case 'GetLineas':
		$json = $o->GetLineas($_GET);
	break;
	case 'GetAll':
		$json = $o->GetAll($_GET);
	break;
	case 'SetAjuste':
		$json = $o->SetAjuste($_GET);
	break;
	case 'SetInspeccion':
		$json = $o->SetInspeccion($_GET);
	break;
	case 'ResetJornada':
		$json = $o->ResetJornada($_GET);
	break;
	case 'DeleteStat':
		$json = $o->DeleteStat($_GET);
	break;
	default:
		$json = $o->Params();
	break;
}

echo json_encode($json);			

?>