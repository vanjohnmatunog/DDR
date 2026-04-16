function Anthem_PreCallBack() {
    var pageHeight = $(window).height();
    var loading = document.createElement("div");
    loading.id = "loading";
    loading.style.display = "table-cell";
    loading.style.height = pageHeight;
    loading.style.width = "100%";
    loading.style.color = "black";
    loading.style.backgroundColor = "#FFFFFF";
    loading.style.filter = "alpha(opacity = 50)";
    loading.style.paddingLeft = "0";
    loading.style.paddingRight = "0";
    loading.style.right = "0";
    loading.style.top = "0";

    loading.style.position = "fixed";
    loading.style.zIndex = "9999";
    loading.style.backgroundImage = "url('../Images/preview.gif')";
    loading.style.backgroundPosition = "center"; 

    loading.style.backgroundRepeat = "no-repeat";
    document.body.appendChild(loading);
}
function Anthem_PostCallBack() {
    var loading = document.getElementById("loading");
    document.body.removeChild(loading);
}