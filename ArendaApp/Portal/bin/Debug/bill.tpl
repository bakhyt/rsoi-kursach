<h1>������ � ������� ������</h1>

<b>{MSG}</b><br><br>

��������� ���� �� �����
<input type=text name=sum value=1000><br>
  <input type=button style="font-weight:bold" value="���������" onclick="doInc()">

<script language=JavaScript>

function doInc() {
  if (document.mainform.sum.value=='') {
    alert("�� ������� ����� ����������") ;
    return ;
  }
  document.mainform.submit() ;
}
</script>

