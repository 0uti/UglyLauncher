<?php

/*
 * filename:    global.php
 * author:      Stephan "Outi" Ihling
 * email:	outi@outi-networks.de
 * date:        04.10.2012
 * last change: 16.11.2013
 */

define('PACK_SERVER', NULL);

// set errorhandler for exeptions
function exception_error_handler($errno, $errstr, $errfile, $errline )
{
  throw new ErrorException($errstr, 0, $errno, $errfile, $errline);
}
set_error_handler("exception_error_handler");



// load config file
include_once(dirname(__FILE__)."/config.php");


// load classes
include_once($global['include_path']."class.uglylauncher.php");


// init classes
try
{
  $DB = new UglyLauncherDB;
  $U = new UglyLauncher;
}
catch(ErrorException $e)
{
  die($e->getMessage());
}
 