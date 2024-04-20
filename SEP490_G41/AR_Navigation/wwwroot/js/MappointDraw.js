
//get MapPoints ByMapId
var mappointList = [];
function getMapPointsByMapId(mapId) {
    $.ajax({
        url: `https://localhost:7186/api/mappoints?filter=mapId eq ${mapId}`,
        method: "get",
    }).then(function (mappointdata) {
        $('#map-list').empty();
        mappointList = []; 
        mappointdata.forEach(function (mappoint) {
            var locationAppParse = parseLocation(mappoint.locationApp);
            mappointList.push({
                mapPointId: mappoint.mapPointId,
                mappointName: mappoint.mappointName,
                locationAppX: locationAppParse.x,
                locationAppY: locationAppParse.y
            });
            const mappointrow = `
                    <tr>
                        <td><input type="checkbox" class="checkbox"></td>
                         <td>#${mappoint.mapPointId}</td>
                         <td>
                       <div class="d-flex align-items-center">
                         <div class="avatar avatar-image avatar-sm m-r-10">
                               <img src="${mappoint.image}" alt="">
                                 </div>
                             <h6 class="m-b-0">${mappoint.mappointName}</h6>
                                 </div>
                        </td>
                      <td>${mappoint.locationWeb}</td>
                          <td>${mappoint.locationApp}</td>
                          <td class="text-right">
                           <button class="btn btn-icon btn-warning btn-hover btn-sm btn-rounded pull-right">
                          <i class="anticon anticon-edit"></i>
                            </button>
                           <button class="btn btn-icon btn-danger btn-hover btn-sm btn-rounded">
                             <i class="anticon anticon-delete"></i>
                          </button>
                        </td>
                      </tr>
                       `;
            // thêm hàng mới vào tbody
            $('#map-list').append(mappointrow);
        });
        renderPoints(mappointList);
        console.log("mappoint legh:", mappointList);
        console.log("mappoint legh:", allPoints);
    }).catch(function (error) {
        console.error("error occurred while fetching mappoint data:", error);
    });
}
function parseLocation(locationWebString) {
    // Loại bỏ dấu ngoặc vuông và dấu cách ở hai đầu chuỗi
    var cleanedString = locationWebString.replace(/^\[|\]$/g, '');

    // Tách chuỗi thành mảng các phần tử, sử dụng dấu phẩy làm dấu phân cách
    var coordinates = cleanedString.split(',');

    // Chuyển đổi phần tử mảng thành số thực
    var x = parseFloat(coordinates[0]);
    var y = parseFloat(coordinates[1]);

    return { x: x, y: y };
}
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
var mapPoint19 = { id: "Ngoi Sao", x: -7.2, y: -11 };

var allPoints = [
    mapPoint0, mapPoint1, mapPoint2, mapPoint3, mapPoint4, mapPoint5,
    mapPoint6, mapPoint7, mapPoint8, mapPoint9, mapPoint10, mapPoint11,
    mapPoint12, mapPoint13, mapPoint14, mapPoint15, mapPoint16, mapPoint17,
    mapPoint18, mapPoint19
];
const sampleEdge = {
    edgeId: "", pointId1: "", pointId2: "", direction: 2, edgeLength: 0
}
const canvas = document.getElementById("canvas_data");
const context = canvas.getContext("2d");
const imgData = context.getImageData(0, 0, canvas.width, canvas.height);
var allEdges = [];

//ratio of image's width, length vs image's pixels

var ratio = 8.682926829;
var root = { id: "root", x: 621, y: 3 };
var radius = 5;

//Start and end point of 1 edge
var beginPoint = { id: "", x: 0, y: 0 };
var endPoint = { id: "", x: 0, y: 0 };
//nearbyPoint checks out if there is a nearby point of begin and end point
//if distance > nearbyPoint radius then input of beginPoint does not accept
var nearbyPoint = { id: "", x: 0, y: 0 };
var numberOfClicks = 0;

//luu y
//toa do trong hinh anh di tu trai - phai, tren - duoi nhu ma tran nen phai doi dau toa do Y
//cac function sau phuc vu cho viec lay toa do 
//toa do trong anh tinh theo goc toa do cua database
function pixelLocation(event) {
    document.getElementById("demo").innerHTML = "Location: (" + (event.offsetX) + ", " + (event.offsetY) + ")" +
        "Location1: (" + (event.offsetX - root.x) + ", " + -(event.offsetY - root.y) + ")";
}
//toa do cua diem trong database
function databaseLocation(event) {
    document.getElementById("demo").innerHTML = document.getElementById("demo").innerHTML +
        " <br>  Location2: (" + (event.offsetX - root.x) / ratio + ", " + -(event.offsetY - root.y) / ratio + ")";
}

//function duoc goi khi bam nut Connect Edge
function setEdge() {
    console.log("SETTING EDGE");
    canvas.setAttribute("onclick", "drawLine(event), count(event)");
}

//function duoc goi khi bam Get Mappoints
function renderPoints() {
    //lay du lieu tu array ben tren
    allPoints.forEach(point => {
        context.beginPath();
        //convert coordinates from image pixels to database coordinates
        let pixelX = point.x * ratio + root.x;
        let pixelY = -point.y * ratio + root.y;

        //draw circle at pixelX,pixelY with radius = 2, start from angle 0, end at 360, counter-clockwise
        context.arc(pixelX, pixelY, radius, 0, 2 * Math.PI, false);
        context.fillStyle = 'red';
        context.fill();
    });
    // saveCanvasState();
}
//phuc vu cho drawLine()
function count() {
    numberOfClicks = numberOfClicks + 1;
}

function drawLine(event) {
    // if it is the 2nd click then it is beginPoint
    if (numberOfClicks == 0) {
        beginPoint.x = event.offsetX;
        beginPoint.y = event.offsetY;
        console.log(beginPoint.x + ", " + beginPoint.y);
        if (inButtonRange(allPoints, beginPoint) == false) {
            console.log("1");
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = { id: "", x: 0, y: 0 };
            return;
        }
        else {
            //khi xac nhan bam dung vao pham vi map point
            inButtonRange(allPoints, beginPoint);
            beginPoint = nearbyPoint;
            console.log("a");
            //ve hinh tron mau xanh 
            context.beginPath();
            context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
            context.closePath();
            context.fillStyle = 'green';
            context.fill();
            nearbyPoint = { id: "", x: 0, y: 0 };
        }
    }
    //3r click will be endPoint
    if (numberOfClicks == 1) {
        endPoint.x = event.offsetX;
        endPoint.y = event.offsetY;
        //ve hinh tron mau do
        context.beginPath();
        context.arc(beginPoint.x * ratio + root.x, -beginPoint.y * ratio + root.y, radius, 0, 2 * Math.PI, false);
        context.closePath();
        context.fillStyle = 'red';
        context.fill();
        //truong hop chon khong dung trong pham vi map point
        if (inButtonRange(allPoints, endPoint) == false) {
            numberOfClicks = -1;
            nearbyPoint = { id: "", x: 0, y: 0 };
            beginPoint = nearbyPoint;
            endPoint = nearbyPoint;
            return;
        }
        else {
            //lay toa do cua map point (button)
            inButtonRange(allPoints, endPoint);
            endPoint = nearbyPoint;
            nearbyPoint = { id: "", x: 0, y: 0 };
            //bat dau ve 1 duong thang giua beginPoint va endPoint
            context.lineWidth = 1;
            context.beginPath();
            //toa do trong hinh anh di tu trai - phai, tren - duoi nhu ma tran nen phai doi dau toa do Y
            let bX = beginPoint.x * ratio + root.x;
            let bY = -beginPoint.y * ratio + root.y;
            let eX = endPoint.x * ratio + root.x;
            let eY = -endPoint.y * ratio + root.y;

            context.moveTo(bX, bY);
            context.lineTo(eX, eY);
            context.stroke();
            //luu gia tri
            saveEdge(beginPoint, endPoint);

            //tra cac bien ve gia tri ban dau de chuan bi cho luot thuc thi tiep theo
            numberOfClicks = -1;
            beginPoint = { id: "", x: 0, y: 0 };
            endPoint = { id: "", x: 0, y: 0 };
            nearbyPoint = { id: "", x: 0, y: 0 };
            canvas.setAttribute("onclick", "");
        }
    }
}

function saveEdge(point1, point2) {
    var edge = {
        edgeId: "1", pointId1: point1.id, pointId2: point2.id, direction: 2, edgeLength: getDistance(point1, point2)
    }
    allEdges.push(edge);
    edge = sampleEdge;
    //luu lai canh vua moi ve
    // const imgData = context.getImageData(0,0,canvas.width,canvas.height);
    saveCanvasState();
    showEdges(allEdges);
    //xoa cac event listener de khong nhan event nao cho den khi bam cac nut functionalities
    canvas.setAttribute("onclick", "");
}

function showEdges(list) {
    document.getElementById("demo").innerHTML = "";
    list.forEach(e => {
        document.getElementById("demo").innerHTML +=
            "<br> Id: " + e.id + ", Start: " + e.pointId1 + ", End: " + e.pointId2 + ", Length: " + e.edgeLength;
    });
}
//Neu trong pham vi cua button thi se tra ve true
//nearbyPoint se lay gia tri cua diem gan nhat neu co
//gia tri se giong trong database

function inButtonRange(allPoints, point) {
    returnPoint = { id: "", x: 0, y: 0 };
    var minDistance = 0.8;
    allPoints.forEach(a => {
        //toa do trong hinh anh di tu trai - phai, tren - duoi nhu ma tran nen phai doi dau toa do 
        let x1 = a.x;
        let x2 = (point.x - root.x) / ratio;
        let y1 = a.y;
        let y2 = -(point.y - root.y) / ratio;

        let distance = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        distance = Math.abs(distance);
        if (distance < minDistance) {
            minDistance = distance;
            returnPoint = a;
        }
    });
    if (returnPoint.id == "") {
        return false;
    }
    else {
        //gan gia tri cho nearbyPoint
        //gia tri nay se duoc dung trong function drawLine()
        nearbyPoint = returnPoint;
        return true;
    }
}

//CAC function lien quan den viec undo
const strokeHistory = [];
function saveCanvasState() {
    strokeHistory.push(canvas.toDataURL()); // Save as data URL
}

// Function to undo the last stroke (connect wrong edge)
function undo() {
    if (strokeHistory.length > 0) {
        strokeHistory.pop(); // Remove the last stroke
        redrawCanvas();
    }
    //khi undo thi array cac edges cung se phai xoa di
    if (allEdges.length > 0) {
        allEdges.pop();
        showEdges(allEdges);
    }
}

// Function to redraw the canvas from strokeHistory
function redrawCanvas() {
    context.clearRect(0, 0, canvas.width, canvas.height); // Clear canvas
    const lastState = new Image();
    lastState.src = strokeHistory[strokeHistory.length - 1];
    lastState.onload = function () {
        context.drawImage(lastState, 0, 0); // Draw the saved state
    };
}

//========================================================


//this function only support mappoints with coordinates in database, not image
function getDistance(point1, point2) {
    return Math.sqrt((point1.x - point2.x) * (point1.x - point2.x) + (point1.y - point2.y) * (point1.y - point2.y));
}

function drawCircle(event) {
    let xX = event.offsetX;
    let yY = event.offsetY;
    context.beginPath();
    context.arc(xX, yY, radius, 0, 2 * Math.PI, false);
    context.lineWidth = 1;
    context.strokeStyle = 'red';
    context.stroke();
}

//=========================================================//=========================================================//
//=========================================================//=========================================================//

var farthestPointImage = { id: "f", x: 0, y: 0 };
var nearbyPointImage = { id: "n", x: 0, y: 0 };

function getFarthestPoint(event) {
    farthestPointImage.x = event.offsetX;
    farthestPointImage.y = event.offsetY;
    document.getElementById("canvas_data").setAttribute("onclick", "");
}
function getNearbyPoint(event) {
    nearbyPointImage.x = event.offsetX;
    nearbyPointImage.y = event.offsetY;
    document.getElementById("canvas_data").setAttribute("onclick", "");
}
function setRoot() {
    canvas.setAttribute("onclick", "chooseRoot(event)");
}
function chooseRoot(event) {
    //function nay chua xong , neu xong thi se lien quan den get Ratio()
    root.x = event.offsetX;
    root.y = event.offsetY;
    document.getElementById("demo1").innerHTML = "Root: " + root.x + ", " + root.y;
    getRatio();
    document.getElementById("demo1").innerHTML = document.getElementById("demo1").innerHTML + " <br> Ratio: " + ratio;
}
function getRatio(farthestPointImage, farthestPointDatabase, nearbyPointImage, nearbyPointDatabase, rootImage) {
    //farthestPointImage la diem xa nhat tinh tu goc toa do, lay toa do trong anh (khong chinh sua)
    //farthestPointDatabase la farthestPointImage nhung toa do o trong database
    //nearbyPointImage la 1 diem gan voi goc toa do, lay toa do trong anh (khong chinh sua)
    //nearbyPointDatabase la nearbyPointImage nhung toa do o trong database

    //rootImage la toa do cua goc toa do o tren anh

    //4 diem tren co the tinh duoc ti le cua (canh dai nhat tren anh, canh dai nhat tren database)
    //va ti le cua (canh gan nhat tren anh, canh gan nhat tren database)
    //lay tong cua ti le do chia 2.05 de uoc luong dong deu
    //2.05 co the tuy chinh

    let p1X = farthestPointImage.x - root.x;
    let p1Y = farthestPointImage.y - root.y;
    let p2X = farthestPointDatabase.x;
    let p2Y = farthestPointDatabase.y;
    let p3X = nearbyPointImage.x - root.x;
    let p3Y = nearbyPointImage.y - root.y;
    let p4X = nearbyPointDatabase.x;
    let p4Y = nearbyPointDatabase.y;

    let parameter1 = Math.sqrt(p1X * p1X + p1Y * p1Y);
    let parameter2 = Math.sqrt(p2X * p2X + p2Y * p2Y);
    let parameter3 = Math.sqrt(p3X * p3X + p3Y * p3Y);
    let parameter4 = Math.sqrt(p4X * p4X + p4Y * p4Y);

    let result = ((parameter1 / parameter2) + (parameter3 / parameter4)) / 2.05;
    return result
}
//==================================================================

function getRatio() {
    var irlLength = 125;
    //irl length la tong chieu dai cua anh (tang 1 Al tong chieu dai la 125m)
    //luu y: anh phai duoc cat sat mep nhat co the
    //khong duoc de thua nhieu
    //offsetWidth la chieu dai cua canvas
    //==> ti le bao nhieu pixel/m 
    let x = canvas.offsetWidth;
    ratio = x / irlLength;
}

function resize() {
    var canvas1 = new fabric.Canvas('canvas_data1');
    // Load the background image (you can replace 'your-image.jpg' with your actual image URL)
    fabric.Image.fromURL('../ALF1new.jpg', function (img) {
        // Access the image dimensions
        var scale = canvas1.width / img.width;

        img.set({
            scaleX: scale,
            scaleY: scale,
            originX: 'left',
            originY: 'top'
        });
        // Set canvas dimensions to match the image size
        canvas1.setWidth(canvas1.width * (canvas1.width / img.width));
        canvas1.setHeight(img.height * (canvas1.width / img.width));
        canvas1.setBackgroundImage(img, canvas1.renderAll.bind(canvas1));
        // Add the image to the canvas
        canvas.width = canvas1.width;
        canvas.height = canvas1.height;
        canvas1.setWidth(0);
        canvas1.setHeight(0);
        console.log("canvas: " + canvas.width + ", " + canvas.height);
        console.log("canvas: " + canvas1.width + ", " + canvas1.height);

        canvas.style.backgroundImage = 'url("../ALF1new.jpg")';
    });
}