<TABLE width=100% cellPadding=0 cellSpacing=0><TR><TD align=center>

<table border=0 cellSpacing=5>

<tr><td align=center valign=top style="border:1px solid #808080; border-radius:5px;" bgcolor=#E0E0E0>
<b>����������� �� �����</b>
<hr>
  <table border=0 cellPadding=5>
  <tr>
  <td colspan=2><font color=#FF4040>{MSG}</font></td></tr>
  <tr>
  <td>��� �����</td>
  <td>��� e-mail</td>
  </tr>
  <tr>
  <td><input type=text name=login size=12></td>
  <td><input type=text name=mail size=12></td>
  </tr>
  <tr>
  <td>������</td>
  <td>������ ������</td>
  </tr>
  <tr>
  <td><input type=password name=pass size=12></td>
  <td><input type=password name=pass2 size=12></td>
  </tr>
  <tr>
  <td colspan=2 align=center>
  <input type=button style="font-weight:bold" value="��������� �����������" onclick="doReg()">
  </td>
  </tr>
  <tr>
  <td colspan=2 align=center>
  <br>
  <!-- <input type=button style="font-weight:bold" value="�����" onclick="doAuth()"> !-->
  </td>
  </tr>
  </table>
  
</td>
</tr>

</table>

</TD></TR></TABLE>

<script language=JavaScript>

function doReg() {
  if (document.mainform.login.value=='') {
    alert("�� ������ ����� ������������.") ;
    return ;
  }
  if (document.mainform.pass.value=='') {
    alert("�� ������ ������.") ;
    return ;
  }
  if (document.mainform.pass.value!=document.mainform.pass2.value) {
    alert("������ �� ���������!") ;
    return ;
  }

  document.mainform.submit() ;
}

if ("{REDIR}"=="1")
  window.location.href="/" ;

</script>

