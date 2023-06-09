<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/AddCartResponse.php');
include_once('../utils.php');
try {

    if ($_SERVER['REQUEST_METHOD'] === 'GET') {
        if (!isset($_SESSION['usersessionid'])) {
            Response::CreateResponse(false, "Session not found!", null);
            return;
        }

        $prodId = $_GET['id'];
        $quantity = $_GET['q'];

        if (!Utils::ValidateInt($prodId) || !Utils::ValidateInt($quantity)) {
            Response::CreateResponse(false, "Invalid input value!", null);
            return;
        }


        $currUserId = Utils::FixSessionObject($_SESSION['usersessionid'])['userid'];

        //Check if order entry exists
        $stmt = $conn->prepare("SELECT * FROM tblorderlist WHERE userid = " . $currUserId . " AND prodId = " . $prodId);
        $stmt->execute();
        $result = $stmt->get_result();

        $intQuantity = (int) $quantity;
        if ($result->num_rows > 0) {
            $row = $result->fetch_assoc();
            $sum = $row['quantity'] + $intQuantity;
            if ($intQuantity === 0 || ($intQuantity < 0 && $sum < 0)) {
                Response::CreateResponse(false, "Invalid quantity value!", null);
                return;
            }

            if ($sum === 0) {
                $stmt = $conn->prepare("DELETE FROM tblorderlist WHERE userid = " . $currUserId . " AND prodId = " . $prodId);
                $stmt->execute();
            } else {
                $stmt = $conn->prepare("UPDATE tblorderlist SET quantity = " . $sum . " WHERE userid = " . $currUserId . " AND prodId = " . $prodId);
                $stmt->execute();
            }

            if (mysqli_affected_rows($conn) > 0) {
                $result = $conn->query("SELECT SUM(quantity) as total FROM tblorderlist WHERE userid = " . $currUserId);
                $row = $result->fetch_assoc();
                $q = $row['total'];
                if (!Utils::ValidateInt($q))
                    $q = 0;
                Response::CreateResponse(true, "Success", new AddCartResponse((int) $prodId, $sum, $q));
            } else
                Response::CreateResponse(false, "Failed to update!", null);

            return;
        } else {

            if ($intQuantity <= 0) {
                Response::CreateResponse(false, "Invalid quantity value!", null);
                return;
            }

            //INSERT Entry
            $stmt = $conn->prepare("INSERT INTO tblorderlist VALUES (" . $currUserId . ", " . $prodId . ", " . $quantity . ")");
            if ($stmt->execute()) {
                $result = $conn->query("SELECT SUM(quantity) as total FROM tblorderlist WHERE userid = " . $currUserId);
                $row = $result->fetch_assoc();
                $q = $row['total'];
                if (!Utils::ValidateInt($q))
                    $q = 0;

                Response::CreateResponse(true, "Success", new AddCartResponse((int) $prodId, (int) $quantity, $q));
                return;
            }

        }

        Response::CreateResponse(false, "Failed adding item to cart!", null);
    } else
        Response::CreateResponse(false, "Invalid Request!", null);
} finally {
    $conn->close();
}
?>