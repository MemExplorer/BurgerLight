<?php

class LoginResponse
{
    public $username;

    public $userid;

    public $carttotal;

    function __construct($userid, $username, $carttotal)
    {
        $this->userid = $userid;
        $this->username = $username;
        $this->carttotal = $carttotal;
    }
}

?>