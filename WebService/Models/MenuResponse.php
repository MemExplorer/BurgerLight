<?php
class MenuResponse
{
    public $id;
    public $name;
    public $price;

    function __construct($id, $name, $price)
    {
        $this->id = $id;
        $this->name = $name;
        $this->price = $price;
    }
}
?>