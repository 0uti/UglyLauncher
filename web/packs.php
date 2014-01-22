<?php
include_once "global.php";

if(isset($_GET['player']) && strlen($_GET['player']) != 0) $U->setPlayer($_GET['player']);

$packs = array();
$RES_PACKS = $DB->query("SELECT P.id,P.packname,P.status,P.accessType,P.accessData,V.version as recommended_version  FROM packs P LEFT JOIN versions V ON V.id = P.recommended_version");
while($REC_PACK = $RES_PACKS->fetcharray())
{
  // unset vars
  unset($pack);
  // check for diabled pack
  if(intval($REC_PACK['status']) == 1 && $REC_PACK['recommended_version'] != NULL && $U->CheckAccess($REC_PACK['accessType'],$REC_PACK['accessData']) == true)
  {
   // PACK is OK
   $pack['name'] = $REC_PACK['packname'];
   $pack['recommended_version'] = $REC_PACK['recommended_version'];
   // Get vesions
   $RES_VERSIONS = $DB->query(sprintf("SELECT * FROM versions WHERE pack_id = %d",intval($REC_PACK['id'])));
   while($REC_VERSION = $RES_VERSIONS->fetcharray())
    if($U->CheckAccess($REC_VERSION['accessType'],$REC_VERSION['accessData']) == true) $pack['versions'][] =$REC_VERSION['version'];
   // append pack to packlist if valid version array is found
   if (is_array($pack['versions'])) $packs['packs'][] = $pack; 
  }
}

header('Content-type: application/json');

echo json_encode($packs);
?>