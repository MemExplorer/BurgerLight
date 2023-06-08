<?php
class OrderResponse
{
    public $id;
    public $quantity;
    public $name;
    public $price;

    function __construct($id, $quantity, $name, $price)
    {
        $this->id = $id;
        $this->quantity = $quantity;
        $this->name = $name;
        $this->price = $price;
    }
}


?>