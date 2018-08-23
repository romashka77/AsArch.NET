declare @id_node int
declare @id_type int

set @id_node = 128922
set @id_type = 1954

/*set @id_node = 10428
set @id_type = 2286*/
/*set @id_node = 10615
set @id_type = 2204*/

/*set @id_node = 83269
set @id_type = 2251*/

/*set @id_node = 128805
set @id_type = 2154
*/
select A.*
	,T.OPTIONS as Options
	,T.N_ORDER as NOrder
	,R.ID_NODE2 as RefNode
	,R.CHAR_VALUE as RefCharValue
	,R.N_ORDER as RefNOrder
	,CH.CHAR_VALUE 
	,DA.DATE_VALUE
	,FL.FLOAT_VALUE
	,I.INT_VALUE
	,TE.TEXT_VALUE
	,DE.ID_PARENT	
	,NOD.STR_LABEL
from
	(select DISTINCT /*B.STR_NAME as Alias, */B.ID_ATTR as IdAttr,
		B.IS_DEFAULT  as IsDefault,
		/*B.ID_TYPE as B_ID_TYPE,*/
		C.STR_NAME as NameAttr,
		C.ID_ATTRTYPE as IdAttrType,
		C.IS_VIRTUAL as IsVirtual
		,D.STR_NAME as NameAttrType 
	from
		[pfr_sap].[dbo].[CONTROLS_XML] B,
		[pfr_sap].[dbo].[ATTRS] C,
		[pfr_sap].[dbo].[ATTRTYPES] D
	where B.ID_ATTR=C.ID_ATTR
		and B.IS_DEFAULT=0
		and C.ID_ATTRTYPE=D.ID_ATTRTYPE
		and B.ID_TYPE = @id_type
	) A
left join [pfr_sap].[dbo].[ATTRVAL_CHAR]CH on IdAttr = CH.ID_ATTR and CH.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_DATE]DA on IdAttr = DA.ID_ATTR and DA.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_FLOAT]FL on IdAttr = FL.ID_ATTR and FL.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_INT]I on IdAttr = I.ID_ATTR and I.ID_NODE=@id_node
left join [pfr_sap].[dbo].[ATTRVAL_TEXT]TE on IdAttr = TE.ID_ATTR and TE.ID_NODE=@id_node
left join [pfr_sap].[dbo].[TYPE_ATTR] T on IdAttr = T.ID_ATTR and T.ID_TYPE = @id_type
left join [pfr_sap].[dbo].[REFATTRS] R on IdAttr = R.ID_ATTR and R.ID_NODE1=@id_node
left join [pfr_sap].[dbo].[DEFAULT_VALUES] DE on IdAttr = DE.ID_ATTR and DE.ID_NODE=@id_node
left join [pfr_sap].[dbo].[NODE] NOD on DE.ID_PARENT= NOD.ID_NODE
/*order by NOrder*/
/*where RefNode >0*/
order by	idattr
