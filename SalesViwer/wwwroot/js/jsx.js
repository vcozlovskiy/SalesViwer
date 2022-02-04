var iLoopCounter = 1;
var iMaxLoop = 6;
var iIntervalId;

function BeginPageLoad() {
    location.href = "<%=PageToLoad %>";

    iIntervalId = window.setInterval(
        "iLoopCounter=UpdateProgressMeter(iLoopCounter,iMaxLoop);", 100);
}

function UpdateProgressMeter(iCurrentLoopCounter, iMaximumLoops) {
    var progressMeter = document.getElementById("ProgressMeter")

    iCurrentLoopCounter += 1;
    if (iCurrentLoopCounter <= iMaximumLoops) {
        progressMeter.innerHTML += ".";
        return iCurrentLoopCounter;
    }
    else {
        progressMeter.innerHTML = "";
        return 1;
    }
}

function EndPageLoad() {
    window.clearInterval(iIntervalId);

    var progressMeter = document.getElementById("ProgressMeter")
    progressMeter.innerHTML = "Страница загружена - переадресация";
}

console.log("Work");