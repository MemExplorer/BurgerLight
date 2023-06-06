<?php

class Response
{
    public $success;
    public $response;

    function __construct($s, $m)
    {
        $this->success = $s;
        $this->response = $m;
    }

    public static function CreateResponse($s, $m)
    {
        ob_end_clean();
        echo json_encode(get_object_vars(new Response($s, $m)));
    }
}

?>