<?php

class Response
{
    public $success;
    public $message;
    public $data;
    function __construct($s, $m, $d)
    {
        $this->success = $s;
        $this->message = $m;
        $this->data = $d;
    }

    public static function CreateResponse($s, $m, $d)
    {
        ob_end_clean();
        echo json_encode(get_object_vars(new Response($s, $m, $d)));
    }
}

?>