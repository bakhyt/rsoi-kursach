<html>
<head>
<meta http-equiv="Cache-Control" content="no-cache">
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="content-type" content="text/html; charset=windows-1251" />
<title>Портал подбора и бронирования жилой недвижимости</title>
<style>
  td.std {border:1px solid #808080;} 
  table.std {border:1px solid #808080;} 
</style>
</head>
<body>

<form name=mainform method=get action=portal accept-charset="utf-8">
<input type=hidden name=page value={PAGE}>
<input type=hidden name=logout value=0>
<input type=hidden name=rnd value="{RND}">

<TABLE width=100% cellPadding=0 cellSpacing=0><TR><TD align=center>

<table border=0 cellPadding=0 cellSpacing=0 width=800px>
<tr>
<td align=center>
<b><font color=#404040 style="font-size:20px">Добро пожаловать, {USERNAME}</font></b> &nbsp;&nbsp;|&nbsp;&nbsp;
<a style="display:{AUTHIN}" href="javascript:goAuth()">Авторизоваться</a>
<a style="display:{NOAUTHIN}" href="javascript:doLogout()">Выйти</a>
<span style="display:{NOAUTHIN}"><br>Лицевой счет: {SUM} руб.</span>
</td>
</tr>

<tr>
<td align=center>
<hr>
<a href="javascript:goMain()">Главная</a> &nbsp;&nbsp;|&nbsp;&nbsp;
<a href="javascript:goRooms()">Подбор объектов</a> &nbsp;&nbsp;|&nbsp;&nbsp;
<a href="javascript:goReg()">Регистрация</a> &nbsp;&nbsp;|&nbsp;&nbsp;
<a style="display:{NOAUTHIN}" href="javascript:goBill()">Лицевой счет</a> &nbsp;&nbsp;|&nbsp;&nbsp;
<a href="javascript:goAbout()">О портале</a>
<hr>
</td>
</tr>

<tr>
<td>
{PAGEDATA}
</td>
</tr>
</table>

</TD></TR></TABLE>

<script language=JavaScript>

function goMain() {
   document.mainform.page.value="start" ;
   document.mainform.submit() ;
}

function goRooms() {
   document.mainform.page.value="findrooms" ;
   document.mainform.submit() ;
}

function goAbout() {
   document.mainform.page.value="about" ;
   document.mainform.submit() ;
}

function goReg() {
   document.mainform.page.value="reg" ;
   document.mainform.submit() ;
}

function goAuth() {
   document.mainform.page.value="auth" ;
   document.mainform.submit() ;
}

function goBill() {
   document.mainform.page.value="bill" ;
   document.mainform.submit() ;
}

function doLogout() {
   document.mainform.page.value="" ;
   document.mainform.logout.value="1" ;
   document.mainform.submit() ;
}

</script>

</form>

</body>
</html>

