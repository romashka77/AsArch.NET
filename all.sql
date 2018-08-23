set nocount on
declare @name varchar(128), @substr varchar(128), @column varchar(128)
set @substr = '%3-лицо Контрагенты Ссылка1:1%' --введите значение искомого параметра 
create table #rslt 
(table_name varchar(128), field_name varchar(128), value uniqueidentifier)
 
declare s cursor for select '['+TABLE_SCHEMA+'].['+ table_name+']' as table_name from information_schema.tables where table_type = 'BASE TABLE'
and LEFT(table_name,8)<>'MSmerge_' --and TABLE_NAME='DocumentTypeExpressions'
order by table_name
 
open s
fetch next from s into @name
while @@fetch_status = 0
begin
declare c cursor for 
select column_name as column_name from information_schema.columns 
--where data_type in ('text', 'ntext', 'varchar', 'char', 'nvarchar', 'char', 'sysname') and table_name = @name
where data_type in ('uniqueidentifier') and '['+TABLE_SCHEMA+'].['+table_name+']' = @name
-- set @name = @name
open c
fetch next from c into @column
while @@fetch_status = 0
begin
print 'Processing table - ' + @name + ', column - ' + @column
exec('insert into #rslt select ''' + @name + ''' as Table_name, ''' + @column + ''', ' + @column + 
' from ' + @name + ' WITH (NOLOCK) where ' + @column + ' = ''' + @substr + '''')
fetch next from c into @column
end
close c
deallocate c
fetch next from s into @name
end
select table_name as [Table Name], field_name as [Field Name], count(*) as [<strong>Matches found</strong>] from #rslt
group by table_name, field_name
order by table_name, field_name
--Если нужно, можем отобразить все найденные значения
select * from #rslt order by table_name, field_name
drop table #rslt
close s
deallocate s