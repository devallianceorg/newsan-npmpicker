<?php	
require('../../lib/login.php');
require('lib.php');

$u = new Newsan();
$u->conectar_sql();

header('Content-Type: text/html; charset=utf-8');

$id_linea = $_GET['id_linea'];
$id_stat = $_GET['id_stat'];
$turno = $_GET['turno'];

$params = array(
	'id_linea' => $id_linea,
	'id_stat' => $id_stat,
	'turno' => $turno,
	'mode'=> 'GetFeedersInestables'
);

$JsonServiceUrl = "http://".$_SERVER['HTTP_HOST']."/".dirname($_SERVER['PHP_SELF'])."/jsonservice/npmpicker.php";
$url = file_get_contents($JsonServiceUrl."?".http_build_query($params));
$data  = json_decode($url,true);

if($data['error']) {
	echo $data['error'];
	die;
}

$data = $data['id_linea'];
$data = $data[key($data)];

echo "<script>";
	LoadFeeder($data);
echo "</script>";

?>
