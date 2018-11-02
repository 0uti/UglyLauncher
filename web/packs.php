<?php
include_once "global.php";
if(isset($_GET['player']) && strlen($_GET['player']) != 0) $U->setPlayer($_GET['player']);

$packs = array();
$RES_PACKS = $DB->query("SELECT P.id,P.packname,P.status,P.accessType,P.accessData,V.version as recommended_version  FROM packs P LEFT JOIN versions V ON V.id = P.recommended_version");
while($REC_PACK = $RES_PACKS->fetcharray())
{
  // unset vars
  unset($pack);
  
  // continue on disabled packs
  if(intval($REC_PACK['status']) == 0) continue;
  
  // continue on forbidden packs
  if($U->CheckAccess($REC_PACK['accessType'],$REC_PACK['accessData']) == false) continue;
  
  // overwrite recommended version if pack is "Minecraft"
  if($REC_PACK['packname'] == "Minecraft") $REC_PACK['recommended_version'] = $M->GetLatestRelease();
    
  // continue in packs with no recommended version
  if($REC_PACK['recommended_version'] == NULL) continue;
  
  // PACK is OK
  $pack['name'] = $REC_PACK['packname'];
  $pack['recommended_version'] = $REC_PACK['recommended_version'];
  
  if ($pack['name'] == "Minecraft")
  {
    // add snapshot versions of Minecraft
    $pack['versions'] = $M->getSnapshots();
  }
  
  // Get vesions from DB
  $RES_VERSIONS = $DB->query(sprintf("SELECT * FROM versions WHERE pack_id = %d ORDER by version DESC",intval($REC_PACK['id'])));
  while($REC_VERSION = $RES_VERSIONS->fetcharray())
  {
    if($U->CheckAccess($REC_VERSION['accessType'],$REC_VERSION['accessData']) == true)
    {
      $_version = array();
      $_version['version'] = $REC_VERSION['version'];
      $_version['downloadZip'] = true;
   
      $pack['versions'][] = $_version;
    }
  }
   
  $pack['recommended_version'] = $REC_PACK['recommended_version'];
  
  if($pack['name'] == "Minecraft")
  {
    // add Release vesions of minecraft
    $releases = $M->GetReleases();
    $keys = array_keys($releases);
    for ($i = 0; $i < count($keys); $i ++)
    {
      $pack['versions'][] = $releases[$keys[$i]];
    }
  }
  
  // append pack to packlist if valid version array is found
  if (is_array($pack['versions'])) $packs['packs'][] = $pack; 
}

header('Content-type: application/json');

echo json_encode($packs);