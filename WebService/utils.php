<?php

class Utils
{
    public static function GenerateRandomString($length = 10)
    {
        $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
        $charactersLength = strlen($characters);
        $randomString = '';
        for ($i = 0; $i < $length; $i++) {
            $randomString .= $characters[random_int(0, $charactersLength - 1)];
        }
        return $randomString;
    }

    public static function GenerateUserToken($userId)
    {
        return hash('MD5', $_SERVER['REMOTE_ADDR'] . $userId);
    }

    public static function FixSessionObject($object)
    {
        return json_decode(json_encode(get_object_vars($object)), true);
    }

    public static function ValidateInt($number)
    {
        $number = filter_var($number, FILTER_VALIDATE_INT);
        return ($number !== FALSE);
    }
}

?>