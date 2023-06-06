<?php
include_once('../../Models/response.php');
try {
    session_start();

    // Unset all session variables
    $_SESSION = array();

    // Destroy the session
    session_destroy();

    Response::CreateResponse(true, "Successfully logged out!");
    return;
} catch (Exception $e) {

}
Response::CreateResponse(false, "Failed to logout!");
?>