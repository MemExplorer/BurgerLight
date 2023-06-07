<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/OrderResponse.php');
include_once('../utils.php');
try {

    if (!isset($_SESSION['usersessionid'])) {
        Response::CreateResponse(false, "Session not found!");
        return;
    }

    $currUserId = Utils::FixSessionObject($_SESSION['usersessionid'])['userid'];
    $stmt = $conn->prepare("SELECT prodId, quantity FROM tblorderlist WHERE userid = '" . $currUserId . "'");
    $stmt->execute();

    $result = $stmt->get_result();

    $menulist = [];
    // Check if the user exists
    if ($result->num_rows > 0) {
        while ($row = $result->fetch_assoc()) {
            $menuitem = new OrderResponse($row['prodId'], $row['quantity']);

            //append data to menulist
            $menulist[] = $menuitem;
        }

        Response::CreateResponse(true, $menulist);
    } else
        Response::CreateResponse(false, "Failed to fetch the menu!");

} finally {
    $conn->close();
}
?>