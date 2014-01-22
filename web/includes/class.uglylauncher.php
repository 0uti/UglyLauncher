<?php

/*
 * filename:    filename.db.mysql.php
 * author:      Stephan "Outi" Ihling
 * email:       outi@outi-networks.de
 * date:        21.01.2024
 * last change: 21.01.2014
*/

if(!defined('PACK_SERVER')) exit();
class UglyLauncher
{
  private $player = null;
  
  public function CheckAccess($type,$data)
  {
    switch($type)
    {
      // free pack
      case 0: return true;
  
      // leveled pack
      case 1:
        if($this->player == null) return false;
        return $this->CheckContao($data);
  
      // whitelisted
      case 2:
        if($this->player == null) return false;
        return $this->CheckWhiteList($data);
  
      // default
      default: return false;
    }
  }
                       
  public function setPlayer($player)
  {
    $this->player = $player;
  }

	private function CheckWhiteList($whitelist)
  {
    global $DB, $global;
    // get Whitelist
    $RES_W = $DB->query(sprintf("SELECT count(*) as islisted FROM whitelist W LEFT JOIN whitelist_part P ON P.whitelist_id = W.id WHERE W.id = %d AND P.player = '%s'",intval($whitelist),$this->player));
    $REC_W = $RES_W->fetcharray();
    
    if(intval($REC_W['islisted']) == 1) return true;
    else return false;
  }

  private function CheckContao($groupid)
  {
  	$groups = $this->GetContaoGroups($this->player);
  	
  	foreach($groups as $group)
  	{
  		if($this->ContaoGroupToLevel(intval($group)) >= intval($groupid)) return true;
  	}

    return false;
  }

	private function GetContaoGroups($player)
	{
		global $DBC;
		
		$RES_G = $DBC->query(sprintf("SELECT T.groups FROM mc_pay P LEFT JOIN tl_member T on T.id = P.contao_user_id WHERE upper(minecraft_nick) = upper('%s')",$player));
		$REC_G = $RES_G->fetcharray();
		
		return unserialize($REC_G['groups']);
	}

	private function ContaoGroupToLevel($group)
	{
		switch($group)
		{
			case 1:	return 20; // Free-User
			case 2: return 30; // Pay-User
			case 3: return 50; // Admins
			case 4: return 0;  // default
			case 5: return 10; // Probe-User
			case 6: return 40; // Moderatoren
			default: return 0; // anything else :) i don't know, so default *ugly*
		}
	}


}      