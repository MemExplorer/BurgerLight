<?php

class AddCartResponse
{
    public $id;
    public $newvalue;

    function __construct($id, $newvalue)
    {
        $this->id = $id;
        $this->newvalue = $newvalue;
    }
}

?>