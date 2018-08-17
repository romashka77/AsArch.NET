declare @id_node int
declare @id_type int

set @id_node = 84683
set @id_type = 1954

select A.*
	,CH.CHAR_VALUE 
	,DA.DATE_VALUE
	,FL.FLOAT_VALUE
	,I.INT_VALUE
	,TE.TEXT_VALUE
from
(select B.STR_NAME as Alias, B.ID_ATTR as IdAttr,
    B.IS_DEFAULT  as IsDefault,
	/*B.ID_TYPE as B_ID_TYPE,*/
	C.STR_NAME as NameAttr,
	C.ID_ATTRTYPE as Id_AttrTypr,
	C.IS_VIRTUAL as IsVirtual
	,D.STR_NAME as NameAttrType 
from
	[pfr_sap].[dbo].[CONTROLS_XML] B,
	[pfr_sap].[dbo].[ATTRS] C,
	[pfr_sap].[dbo].[ATTRTYPES] D
	
where B.ID_ATTR=C.ID_ATTR
	and C.ID_ATTRTYPE=D.ID_ATTRTYPE
	and B.ID_TYPE = @id_type) A
left join [pfr_sap].[dbo].[ATTRVAL_CHAR]CH on IdAttr = CH.ID_ATTR and CH.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_DATE]DA on IdAttr = DA.ID_ATTR and DA.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_FLOAT]FL on IdAttr = FL.ID_ATTR and FL.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_INT]I on IdAttr = I.ID_ATTR and I.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_TEXT]TE on IdAttr = TE.ID_ATTR and TE.ID_NODE=@id_node


	
/*SELECT * from(
SELECT
	B.STR_NAME as B_STR_NAME,
	B.ID_ATTR as B_ID_ATTR,
    B.IS_DEFAULT  as B_IS_DEFAULT,
	B.ID_TYPE as B_ID_TYPE,
	C.ID_ATTR as C_ID_ATTR,
	C.STR_NAME as C_STR_NAME,
	C.ID_ATTRTYPE as C_ID_ATTRTYPE,
	C.IS_VIRTUAL as C_IS_VIRTUAL,
	D.ID_ATTRTYPE as D_ID_ATTRTYPE,
	D.STR_NAME as D_STR_NAME 
  FROM [pfr_sap].[dbo].[CONTROLS_XML] B
left join [pfr_sap].[dbo].[ATTRS] C on B.ID_ATTR=C.ID_ATTR
left join [pfr_sap].[dbo].[ATTRTYPES] D on C.ID_ATTRTYPE=D.ID_ATTRTYPE
where ID_TYPE= 1954 )A
left join (select CH.ID_NODE as CH_ID_NODE,
	CH.ID_ATTR as CH_ID_ATTR,
	CH.CHAR_VALUE as CH_CHAR_VALUE
 from [pfr_sap].[dbo].[ATTRVAL_CHAR]CH
where (ID_NODE =84683))CHA on B_ID_ATTR = CH_ID_ATTR
left join (select DA.ID_NODE as DA_ID_NODE,
	DA.ID_ATTR as DA_ID_ATTR,
	DA.DATE_VALUE as DA_DATE_VALUE
 from [pfr_sap].[dbo].[ATTRVAL_DATE]DA
where (ID_NODE =84683))DAT on B_ID_ATTR = DA_ID_ATTR

left join (select FL.ID_NODE as FL_ID_NODE,
	FL.ID_ATTR as FL_ID_ATTR,
	FL.FLOAT_VALUE as FL_FLOAT_VALUE
 from [pfr_sap].[dbo].[ATTRVAL_FLOAT]FL
where (ID_NODE =84683))FLO on B_ID_ATTR = FL_ID_ATTR

left join (select I.ID_NODE as I_ID_NODE,
	I.ID_ATTR as I_ID_ATTR,
	I.INT_VALUE as I_INT_VALUE
 from [pfr_sap].[dbo].[ATTRVAL_INT]I
where (ID_NODE =84683))INTE on B_ID_ATTR = I_ID_ATTR

left join (select TE.ID_NODE as TE_ID_NODE,
	TE.ID_ATTR as TE_ID_ATTR,
	TE.TEXT_VALUE as TE_TEXT_VALUE
 from [pfr_sap].[dbo].[ATTRVAL_TEXT]TE
where (ID_NODE =84683))TEX on B_ID_ATTR = TE_ID_ATTR

left join (select FA.ID_ATTR as FA_ID_ATTR,
	FA.N_ORDER as FA_N_ORDER,
	FA.ID as FA_ID,
	FA.ID_PARENT as FA_ID_PARENT,
	FA.STR_NAME as FA_STR_NAME
 from [pfr_sap].[dbo].[FACENAV] FA
)FAC on B_ID_ATTR = FA_ID_ATTR
order by C_ID_ATTR*/