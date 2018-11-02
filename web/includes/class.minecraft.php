<?php

/*
 * filename:    class.minecraft.php
 * author:      Stephan "Outi" Ihling
 * email:       outi@outi-networks.de
 * date:        01.11.2018
 * last change: 01.11.2018
 */
    
if(!defined('PACK_SERVER')) exit();

class Minecraft
{
  var $_url = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
  var $_data = array();
  var $_snapshots = array();
  var $_releases = array();
  
  public function __construct()
  {
    $this->GetJsonData();
    $this->GetVersions();


  }
  
  
  private function GetJsonData()
  {
    $JSON = file_get_contents($this->_url);
    $this->_data = json_decode($JSON,true);
  }


  private function GetVersions()
  {
    $versions = $this->_data['versions'];
    $keys = array_keys($versions);
    
    for ($i = 0; $i < count($keys); $i++)
    {
      $v = $versions[$keys[$i]];
      if ($v['type'] == "snapshot")
      { 
        $snapshot = array();
        $snapshot['version'] = $v['id'];
        $snapshot['downloadZip'] = false;
        $this->_snapshots[] = $snapshot;
      }
      if ($v['type'] == "release")
      {
        $release = array();
        $release['version'] = $v['id'];
        $release['downloadZip'] = false;
        $this->_releases[] = $release;
      } 
    }
  }

  public function GetSnapshots()
  { 
    return $this->_snapshots;
  }

  public function GetReleases()
  {
    return $this->_releases;
  }

  public function getLatestRelease()
  {
    return $this->_data['latest']['release'];
  }
}     