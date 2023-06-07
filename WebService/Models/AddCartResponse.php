<?php

class AddCartResponse
{
    public $id;
    public $newvalue;
    public $total;

    function __construct($id, $newvalue, $total)
    {
        $this->id = $id;
        $this->newvalue = $newvalue;
        $this->total = $total;
    }
}

?>