<h1>�������� ������ ������������</h1>

�������� ����� ��� ��������� �������:
 <select name=citylist>{CITYLIST}</select>
 <input type=button name=filter value="�������" onClick="doFilter()">

<table border=1 cellPadding=2 cellSpacing=0>
<tr bgcolor=#808080>
<td align=center style="font-weight:bold">���</td>
<td align=center style="font-weight:bold">�����</td>
<td align=center style="font-weight:bold">�����</td>
<td align=center style="font-weight:bold">���</td>
<td align=center style="font-weight:bold">����</td>
<td align=center style="font-weight:bold">�������</td>
<td align=center style="font-weight:bold">�����</td>
<td align=center style="font-weight:bold">&nbsp;</td>
</tr>
{ROOMS}
</table>

<input type=hidden name=selid>
<script language=JavaScript>

function doBrony(id) {
   if ("{USER_ID}"=="") {
     alert("���������� ���� ������������������ �������������!") ;
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
