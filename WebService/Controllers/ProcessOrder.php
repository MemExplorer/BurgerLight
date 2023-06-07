<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/AddCartResponse.php');
include_once('../utils.php');

try {

    if (!isset($_SESSION['usersessionid'])) {
        Response::CreateResponse(false, "Session not found!");
        return;
    }

    $currUserId = Utils::FixSessionObject($_SESSION['usersessionid'])['userid'];
    $query = "INSERT INTO tblcheckout (userid, prodId, quantity) SELECT userid, prodId, quantity FROM tblorderlist WHERE userid = " . $currUserId . "; DELETE FROM tblorderlist WHERE userid = " . $currUserId . ";";

    if (mysqli_multi_query($conn, $query)) {
        // Success
        Response::CreateResponse(true, "Success");
    } else {
        // Error
        Response::CreateResponse(false, "Failed to process order!");
    }

} finally {
    $conn->close();
}
?>