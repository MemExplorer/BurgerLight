<?php
class MenuResponse
{
    public $id;
    public $name;

    public $image;
    public $price;

    function __construct($id, $name, $image, $price)
    {
        $this->id = $id;
        $this->name = $name;
        $this->image = $image;
        $this->price = $price;
    }
}
?>