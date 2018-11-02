<?php
/*
 * filename:    class.db_mysqli.php
 * author:      Stephan Ihling	
 * date:        30.08.2015
 * last change: 30.08.2015
*/

if(!defined('PACK_SERVER')) exit();
class dbclass extends mysqli {
	private $sLastSQL = "";
	public $iQueryCounter = 0;
	
	// constructor
	public function __construct()
	{
		try
		{
			parent::__construct($this->server, $this->user, $this->password, $this->database);
			parent::set_charset($this->charset);
		}
		catch(ErrorException $e)
		{
			parent::close();
			throw $e;
		}
	}
	
	// destructor
	public function __destruct()
	{
		parent::close();
	}
	
	public final function query($SQL, $resultmode = MYSQLI_STORE_RESULT)
	{
		try
		{
			if($GLOBALS['global']['debug']) debug_info("DATABASE::query -> ".$SQL);
			$result = parent::query($SQL,$resultmode);
			$this->sLastSQL = $SQL;
			if($result===false) throw new ErrorException($this->error);
			$this->iQueryCounter++;
			return new Result($result,$this);
		}
		catch(ErrorException $e)
		{
			if ($GLOBALS['global']['debug']) debug_info("DATABASE::query -> ERR:".$e->getMessage());
			throw $e;
		}
		
	}
	
}


final class Result {
	private $insertID;
	private $cleard=false;
	private $result;
	
	public function __construct($result,$mysqli_instance)
	{
		$this->result=$result;
		$this->insertID=mysqli_insert_id($mysqli_instance);
		// have result ?
		if (is_bool($result)) $this->cleard = true;
	}
	
	public function __destruct()
	{
		if ($this->cleard == false) $this->clear();
	}
	
	public function clear()
	{
		// $this->result->free();
		$this->cleard == true;
	}
	
	public function insertID()
	{
		return $this->insertID;
	}
	
	public function numRows()
	{
		return $this->result->num_rows;
	}
	
	public function fetchArray()
	{
		if ($this->cleard == true) return false;
		return $this->result->fetch_array();
	}
}