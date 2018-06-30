<h1>Бронирование недвижимости</h1>

<input type=hidden name=selid value={SELID}>
<input type=hidden name=isok value=1>

<b>{MSG}</b><br><br>

<b>Параметры квартиры<b>

<table border=1 cellPadding=2 cellSpacing=0>
<tr><td>Код</td><td>{ID}</td></tr>
<tr><td>Город</td><td>{CITY}</td></tr>
<tr><td>Адрес</td><td>{ADDRESS}</td></tr>
<tr><td>Тип</td><td>{TYPE}</td></tr>
<tr><td>Цена</td><td>{PRICE} р/месяц</td></tr>
<tr><td>Площадь</td><td>{S} кв.м</td></tr>
<tr><td>Класс</td><td>{ELITE}</td></tr>
</tr>
<table>
<br>
<input type=button style="font-weight:bold" value="Забронировать" onclick="doRes()">

<script language=JavaScript>

function doRes() {
  document.mainform.submit() ;
}

if ("{REDIR}"=="1")
  window.location.href="/?page=findrooms" ;

</script>

