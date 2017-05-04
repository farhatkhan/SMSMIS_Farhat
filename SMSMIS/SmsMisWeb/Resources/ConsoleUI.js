function buildMenu() {
    var parentCell = document.getElementById('parentMenuContainer');
    var html = '<table border="0" class="toolbarItems"><tr>'
    for(lnk in linkParents) {
        pL = linkParents[lnk];
        html += '<td class="topMenuItem" id="' + lnk + '"'
        if (pL.url != null) html += ' onclick="popupMenuHide();location=\'' + pL.url + '\';"'
        html += ' onmouseover="popupMenuKillHideTimer();renderChildMenu(this,' + pL.parentId + ');"';
        html += '>' + pL.parentText + '</td><td class="divider"></td>'
    }
    html += '</tr></table>';
    parentCell.innerHTML = html;
}
function renderChildMenu(srcObj, parentId) {
    var objPL = linkParents['lp_' + parentId];
    
    if (typeof lastTdHovered != 'undefined' && lastTdHovered != null) {
        if (lastTdHovered.id == srcObj.id) return;
        lastTdHovered.className = 'topMenuItem';
    }
    lastTdHovered = srcObj;
    var objDv = document.getElementById('tblPopup');
    if (typeof objDv == 'undefined') return;
    objDv.style.display = 'none';

    if (typeof objPL != 'undefined' && objPL != null) {
        if (typeof objPL.subLinks == 'undefined' || objPL.subLinks == null) return;
        if (typeof objPL.subItemsCache == 'undefined') {
            var html = '<table border="0" class="toolislandLinks" onmouseover="popupMenuKillHideTimer()" onmouseout="popupMenuInitHideTimer()">'
            for (lnkId in objPL.subLinks) {
                objLnk = objPL.subLinks[lnkId];
                html += '<tr class="toolRow"><td onclick="popupMenuHide();location=\'' + objLnk.url + '\';">' + objLnk.linkText + '</td></tr>';
            }
            html += '<tr><td style="padding:0px;">'
            html += '<table border="0" class="toolislandbottom"><tr><td class="leftCell">&nbsp;</td><td class="middleCell">&nbsp;</td><td class="rightCell">&nbsp;</td></tr></table></td>'
            html += '</tr></table>'
            objPL.subItemsCache = html;
        }

        srcObj.onmouseout = popupMenuInitHideTimer;
        objDv.innerHTML = objPL.subItemsCache;
        var pos = $(srcObj).offset();
        objDv.style.left = (pos.left) + 'px';
        objDv.style.top = (pos.top + 40) + 'px';
        srcObj.className = 'topMenuItemHover';
        $(objDv).slideDown(200);
    }
}
function popupMenuInitHideTimer() {
    if (popupMenuTimerInstance != null) window.clearTimeout(popupMenuTimerInstance);
    popupMenuTimerInstance = window.setTimeout(popupMenuHide, 500);
}
function popupMenuKillHideTimer() {
    if (popupMenuTimerInstance != null) window.clearTimeout(popupMenuTimerInstance);
}
function popupMenuHide() {
    var objDv = document.getElementById('tblPopup');
    if (typeof objDv == 'undefined') return;
    $(objDv).fadeOut(250);
    if (typeof lastTdHovered != 'undefined' && lastTdHovered != null) lastTdHovered.className = 'topMenuItem';
    lastTdHovered = null;
}
var popupMenuTimerInstance = null;
function addLink(parentId, linkId, linkText, url) {
    var objLink = { 'parentId': parentId, 'linkId': linkId, 'linkText': linkText, 'url': url };
    this.allLinks['lnk_' + linkId] = objLink;
    var objPL = linkParents['lp_' + parentId];
    if (typeof objPL != 'undefined' && objPL != null) {
        if (typeof objPL.subLinks == 'undefined' || objPL.subLinks == null) objPL.subLinks = new Array();
        objPL.subLinks['lnk_' + linkId] = objLink;
    }
}
function addParent(parentId, parentText, url) {
    linkParents['lp_' + parentId] = { 'parentId': parentId, 'parentText': parentText, 'url': url };
}
var allLinks = new Array();
var linkParents = new Array();
function loadMenuEntries() {
    $.ajax({
        url: "/UI/buildParentMenu",
        context: document.body
    }).done(function (data) {
        for (var x = 0; x < data.length; x++) {
            lnk = data[x];
            if (lnk.parentId == 0) {
                addParent(lnk.linkID, lnk.linkLabel, lnk.url);
            }
            else {
                addLink(lnk.parentId, lnk.linkID, lnk.linkLabel, lnk.url);
            }
        }
        buildMenu();
    });
}
$(document).ready(loadMenuEntries);
//addParent(1, 'Manage', null);
//addParent(2, 'Configuration', null);
//addParent(3, 'Logout', '/UI/Logout');

//addLink(1, 1, 'Regions', '/UI/Regions');
//addLink(1, 2, 'Branches', '/UI/Branches');
//addLink(1, 3, 'Locations', '/UI/Locations');
//addLink(1, 4, 'Departments', '/UI/Departments');
//addLink(2, 5, 'Clients', '/UI/Clients');
//addLink(2, 6, 'Change Passowrd', '/UI/Password');
