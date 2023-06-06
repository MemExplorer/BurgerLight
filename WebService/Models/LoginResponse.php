<?php

class LoginResponse
{
    public $username;

    public $userid;

    function __construct($userid, $username)
    {
        $this->userid = $userid;
        $this->username = $username;
    }
}

?>