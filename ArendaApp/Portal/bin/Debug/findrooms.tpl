<h1>Страница поиска недвижимости</h1>

Выберите город для уточнения запроса:
 <select name=citylist>{CITYLIST}</select>
 <input type=button name=filter value="Выбрать" onClick="doFilter()">

<table border=1 cellPadding=2 cellSpacing=0>
<tr bgcolor=#808080>
<td align=center style="font-weight:bold">Код</td>
<td align=center style="font-weight:bold">Город</td>
<td align=center style="font-weight:bold">Адрес</td>
<td align=center style="font-weight:bold">Тип</td>
<td align=center style="font-weight:bold">Цена</td>
<td align=center style="font-weight:bold">Площадь</td>
<td align=center style="font-weight:bold">Класс</td>
<td align=center style="font-weight:bold">&nbsp;</td>
</tr>
{ROOMS}
</table>

<input type=hidden name=selid>
<script language=JavaScript>

function doBrony(id) {
   if ("{USER_ID}"=="") {
     alert("Необходимо быть зарегистрированным пользователем!") ;
   }
   else {
     document.mainform.page.value="reserve" ;
     document.mainform.selid.value=id ;
     document.mainform.submit() ;
   }
}

function doFilter() {
   document.mainform.page.value="findrooms" ;
   document.mainform.submit() ;
}

</script>
