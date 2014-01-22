<?php

/*
 * filename:	filename.db.mysql.php
 * author:	Stephan "Outi" Ihling
 * email:	outi@outi-networks.de
 * date:	21.01.2024
 * last change:	21.01.2014
 */
 
if(!defined('PACK_SERVER')) exit();
class dbclass
{
  private $iConID = NULL;
  private $sLastSQL = "";
  public $iQueryCounter = "";
  
  // contructor
  public function __construct()
  {
    try
    {
      $this->connect();
    }
    catch(ErrorException $e)
    {
      throw $e;
    }
  }
  
  // destructor
  public function __destruct()
  {
    $this->close();
  }
  
  // connect to DB
  final public function connect()
  {
    try
    {
      $this->iConID = mysql_connect($this->server, $this->user, $this->password, $this->newlink);
      if(!mysql_set_charset($this->charset, $this->iConID)) throw new ErrorException(mysql_error($this->iConID)); // mysql function has no exceptions
      if(!mysql_select_db($this->database)) throw new ErrorException(mysql_error($this->iConID)); // mysql function has no exceptions
    }
    catch(ErrorException $e)
    {
      $this->close();
      throw $e;
    }
  }
  
  // close database
  final public function close()
  {
    if($this->iConID != NULL) mysql_close($this->iConID);
    $this->iConID = NULL;
  }
  
  // query database
  final public function query($SQL)
  {
    try
    {
      $result = mysql_query($SQL, $this->iConID);
      $this->sLastSQL = $SQL;
      if($result === false) throw new ErrorException(mysql_error($this->iConID));
      $this->iQueryCounter++;
      return new Result($result,$this->iConID);
    }
    catch(ErrorException $e)
    {
      throw $e;
    }
  }
}

final class Result
{
  private $result;
  private $serverID;
  private $insertID;
  private $cleared=false;
  
  // contructor
  public function __construct($result, $serverID)
  {
    $this->result = $result;
    $this->serverID = $serverID;
    $this->insertID = mysql_insert_id($serverID);
    // have result ?
    if($result == "1") $this->cleared = true;
  }
  
  // destructor
  public function __destruct()
  {
    if($this->cleared == false) $this->clear();
  }
  
  // clear result
  public function clear()
  {
    @mysql_free_result($this->result);
    $this->clared = true;
  }
  
  // get insert id
  public function insertID()
  {
    return $this->insertID;
  }
  
  // get num rows
  public function numRows()
  {
    return mysql_num_rows($this->result);
  }
  
  // fetch datarecors as array
  public function fetchArray()
  {
    if ($this->cleared == true) return false;
    return mysql_fetch_array($this->result);
  }
}
