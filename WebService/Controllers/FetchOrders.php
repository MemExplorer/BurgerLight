<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/OrderResponse.php');
include_once('../utils.php');
try {

    if (!isset($_SESSION['usersessionid'])) {
        Response::CreateResponse(false, "Session not found!", null);
        return;
    }

    $currUserId = Utils::FixSessionObject($_SESSION['usersessionid'])['userid'];
    $stmt = $conn->prepare("SELECT a.prodId, a.quantity, b.prodName, b.price FROM tblorderlist a INNER JOIN tblmenulist b ON a.prodId = b.prodId WHERE userid = '" . $currUserId . "'");
    $stmt->execute();

    $result = $stmt->get_result();

    $orderlist = [];
    // Check if the user exists
    if ($result->num_rows > 0) {
        while ($row = $result->fetch_assoc()) {
            $orderitem = new OrderResponse($row['prodId'], $row['quantity'], $row['prodName'], $row['price']);

            //append data to orderlist
            $orderlist[] = $orderitem;
        }
    }

    Response::CreateResponse(true, "Success", $orderlist);

} finally {
    $conn->close();
}
?>