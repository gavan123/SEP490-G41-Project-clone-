//pre-define map-points that represent data from database
var mapPoint0 = { id: "p.R102", x: 19.13, y: -7.61 };
var mapPoint1 = { id: "p.R104", x: 30.74, y: -7.61 };
var mapPoint2 = { id: "p.R106", x: 35.74, y: -7.61 };
var mapPoint3 = { id: "p.R103", x: 28, y: -10.15 };
var mapPoint4 = { id: "p.R105", x: 33.65, y: -10.15 };
var mapPoint5 = { id: "p.TS", x: 16.8, y: -11.65 };
var mapPoint6 = { id: "p.R107", x: 40, y: -11.44 };
var mapPoint7 = { id: "p.kt", x: 40.575, y: -7.61 };
var mapPoint8 = { id: "Tuong Coc", x: 5.58, y: -13.5 };
var mapPoint9 = { id: "WC", x: 10.5, y: -1.2 };
var mapPoint10 = { id: "Cau Thang 1", x: 3.7, y: -1.16 };
var mapPoint11 = { id: "p.L108", x: -69.2, y: -7.4 };
var mapPoint12 = { id: "p.L106", x: -60, y: -7.4 };
var mapPoint13 = { id: "p.L104", x: -55.23, y: -7.4 };
var mapPoint14 = { id: "p.L102", x: -44.75, y: -7.4 };
var mapPoint15 = { id: "p.L107", x: -68, y: -10 };
var mapPoint16 = { id: "p.L105", x: -62, y: -10 };
var mapPoint17 = { id: "p.L103", x: -53, y: -10 };
var mapPoint18 = { id: "p.L101", x: -37.84, y: -13.83 };
var mapPoint19 = { id: "xxx", x: -39.3, y: -8.6 };

const sampleEdge = {
    edgeId: "", pointId1: "", pointId2: "", direction: 2, edgeLength: 0
}

var allPoints = [
    mapPoint0, mapPoint1, mapPoint2, mapPoint3, mapPoint4, mapPoint5,
    mapPoint6, mapPoint7, mapPoint8, mapPoint9, mapPoint10, mapPoint11,
    mapPoint12, mapPoint13, mapPoint14, mapPoint15, mapPoint16, mapPoint17, mapPoint18, mapPoint19
];

//ratio of image's width, length vs image's pixels
const ratioX = 8.62;
const ratioY = 8.62;

var root = { x: 621, y: 3 };
var beginPoint = { id: "1", x: 0, y: 0 }
var endPoint = { id: "2", x: 0, y: 0 }
var numberOfClicks = -1;

function pixelLocation(event) {
    document.getElementById("demo").innerHTML = "Location Web: (" + (event.offsetX) + ", " + (event.offsetY) + ")";
}
function databaseLocation(event) {
    document.getElementById("demo").innerHTML = document.getElementById("demo").innerHTML +
        " <br>  Location App: (" + (event.offsetX - root.x) / 8.4 + ", " + -(event.offsetY - root.y) / 8.62 + ")";
}
function setRoot(event) {
    root.x = event.offsetX;
    root.y = event.offsetY;
    const mainCanvas = document.getElementById("canvas_data");
    mainCanvas.setAttribute("onclick", "")
}


function renderPoints(event, element) {
    if (numberOfClicks == -1) {
        const ctx = element.getContext('2d');
        allPoints.forEach(point => {
            ctx.beginPath();
            //convert coordinates from image pixels to database coordinates
            let pixelX = point.x * ratioX + root.x;
            let pixelY = -point.y * ratioY + root.y;

            //draw circle at pixelX,pixelY with radius = 2, start from angle 0, end at 360, counter-clockwise
            ctx.arc(pixelX, pixelY, 3, 0, 2 * Math.PI, false);
            ctx.fillStyle = 'red';
            ctx.fill();
            ctx.lineWidth = 1;
            ctx.strokeStyle = 'red';
            ctx.stroke();
        });
    }
}

function count() {
    numberOfClicks = numberOfClicks + 1;
}

function drawCircle(event, element) {
    // if it is not the first click then draw a circle at cursor location
    if (numberOfClicks > -1) {
        let xX = event.offsetX;
        let yY = event.offsetY;
        const ctx = element.getContext('2d');
        ctx.beginPath();
        ctx.arc(xX, yY, 3, 0, 2 * Math.PI, false);
        ctx.lineWidth = 1;
        ctx.strokeStyle = 'red';
        ctx.stroke();
    }
}
function drawLine(event, element) {
    var rect = element.getBoundingClientRect(); 
    var scaleX = element.width / rect.width; // Tính tỷ lệ co giãn theo chiều ngang
    var scaleY = element.height / rect.height; // Tính tỷ lệ co giãn theo chiều dọc

    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = (event.clientX - rect.left) * scaleX; 
        beginPoint.y = (event.clientY - rect.top) * scaleY; 
    }
    //3rd click will be endPoint
    if (numberOfClicks == 1) {
        endPoint.x = (event.clientX - rect.left) * scaleX; 
        endPoint.y = (event.clientY - rect.top) * scaleY;

        const ct_var = element.getContext('2d');
        ct_var.strokeStyle = 'black';
        ct_var.lineWidth = 1;
        ct_var.beginPath();
        ct_var.moveTo(beginPoint.x, beginPoint.y);
        ct_var.lineTo(endPoint.x, endPoint.y);
        ct_var.stroke();

        beginPoint = nearestPoint(allPoints, beginPoint);
        endPoint = nearestPoint(allPoints, endPoint);
        saveEdge(beginPoint, endPoint);
        numberOfClicks = -1;
        beginPoint = { id: "", x: 0, y: 0 };
        endPoint = { id: "", x: 0, y: 0 };
    }
}

function saveEdge(point1, point2) {
    console.log("EDGE SAVED: " + point1.id + "-" + point2.id);

    edge = {
        edgeId: "1", pointId1: point1.id, pointId2: point2.id, direction: 2, edgeLength: getDistance(point1, point2)
    }

    // sendDataToApi(edge);  
    edge = sampleEdge;
}

function nearestPoint(allPoints, point) {
    returnPoint = { id: "", x: 0, y: 0 };
    var minDistance = 4;
    allPoints.forEach(a => {

        let x1 = a.x;
        let x2 = (point.x - root.x) / 8.4;
        let y1 = a.y;
        let y2 = -(point.y - root.y) / 8.62;

        let distance = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        distance = Math.abs(distance);
        if (distance < minDistance) {
            minDistance = distance;
            returnPoint = a;
        }
    });
    return returnPoint;
}
//this function only support mappoints with coordinates in database, not image
function getDistance(point1, point2) {
    return Math.sqrt((point1.x - point2.x) * (point1.x - point2.x) + (point1.y - point2.y) * (point1.y - point2.y));
}

function getRatio(point1, point2, point3) {
    //point1 will be coordinates of 1 point in the database 
    //point2 will be coordinates of point1 in image pixels 
    //both need to have the same coordinate origin 
    //point3 is the coordinates of the root in the image

    return Math.sqrt(point1.x * point1.x + point1.y * point1.y) / Math.sqrt((point2.x - point3.x) * (point2.x - point3.x) + (point2.y - point3.y) * (point2.y - point3.y));

}

function sendDataToApi(data) {
    $.ajax({
        url: "",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (response) {
            console.log("Data sent successfully:", response);
        },
        error: function (xhr, status, error) {
            console.error("Error sending data:", error);
        }
    });
}