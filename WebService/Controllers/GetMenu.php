<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/MenuResponse.php');
include_once('../utils.php');

try {

    if (!isset($_SESSION['usersessionid'])) {
        Response::CreateResponse(false, "Session not found!");
        return;
    }

    $stmt = $conn->prepare("SELECT * FROM tblmenulist");
    $stmt->execute();

    $result = $stmt->get_result();

    $menulist = [];
    // Check if the user exists
    if ($result->num_rows > 0) {
        while ($row = $result->fetch_assoc()) {
            $menuitem = new MenuResponse($row['prodId'], $row['prodName'], $row['price']);

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