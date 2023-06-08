<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/MenuResponse.php');
include_once('../utils.php');

try {

    if (!isset($_SESSION['usersessionid'])) {
        Response::CreateResponse(false, "Session not found!", null);
        return;
    }

    $stmt = $conn->prepare("SELECT * FROM tblmenulist");
    $stmt->execute();

    $result = $stmt->get_result();

    $menulist = [];
    // Check if the user exists
    if ($result->num_rows > 0) {
        while ($row = $result->fetch_assoc()) {
            $menuitem = new MenuResponse($row['prodId'], $row['prodName'], $row['prodImage'], $row['price']);

            //append data to menulist
            $menulist[] = $menuitem;
        }

        Response::CreateResponse(true, "Success", $menulist);
    } else
        Response::CreateResponse(false, "No items in menu.", $menulist);



} finally {
    $conn->close();
}
?>