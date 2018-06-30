<h1>Работа с лицевым счетом</h1>

<b>{MSG}</b><br><br>

Пополнить счет на сумму
<input type=text name=sum value=1000><br>
  <input type=button style="font-weight:bold" value="Пополнить" onclick="doInc()">

<script language=JavaScript>

function doInc() {
  if (document.mainform.sum.value=='') {
    alert("Не указана сумма пополнения") ;
    return ;
  }
  document.mainform.submit() ;
}
</script>

