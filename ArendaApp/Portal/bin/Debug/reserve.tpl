<h1>������������ ������������</h1>

<input type=hidden name=selid value={SELID}>
<input type=hidden name=isok value=1>

<b>{MSG}</b><br><br>

<b>��������� ��������<b>

<table border=1 cellPadding=2 cellSpacing=0>
<tr><td>���</td><td>{ID}</td></tr>
<tr><td>�����</td><td>{CITY}</td></tr>
<tr><td>�����</td><td>{ADDRESS}</td></tr>
<tr><td>���</td><td>{TYPE}</td></tr>
<tr><td>����</td><td>{PRICE} �/�����</td></tr>
<tr><td>�������</td><td>{S} ��.�</td></tr>
<tr><td>�����</td><td>{ELITE}</td></tr>
</tr>
<table>
<br>
<input type=button style="font-weight:bold" value="�������������" onclick="doRes()">

<script language=JavaScript>

function doRes() {
  document.mainform.submit() ;
}

if ("{REDIR}"=="1")
  window.location.href="/?page=findrooms" ;

</script>

