WITH Refobjects 
    AS 
    (
        SELECT
        o.name AS ReferencingObject,
        ref.referenced_entity_name AS ReferencedObject,
		1 as Stack,
		referenced_schema_name AS sch
        FROM
        sys.dm_sql_referenced_entities('dbo.'+'{uspname}', 'OBJECT') ref
        INNER JOIN
        sys.objects o ON ref.referenced_id = o.[object_id]
        WHERE
        ref.referenced_entity_name like 'USP%' --sed.referenced_entity_name = 'USP_EODProcessing_CHDN' --and  sed.referenced_entity_name like 'USP%' --'USP_EODProcessing_CHDN'--'USP_SaveCaseXML_Process_ATON' --''USP_PopulateCHDNProcessingOptions' 
      
	  UNION ALL 
        SELECT
            r.ReferencedObject AS ReferencingObject,
            o.name as ReferencedObject,
			r.Stack + 1,
			referenced_schema_name AS sch
        FROM Refobjects as r  cross apply
            sys.dm_sql_referenced_entities(r.sch +'.' + r.ReferencedObject, 'OBJECT') ref
            INNER JOIN
            sys.objects o ON ref.referenced_id = o.[object_id]
			where ref.referenced_entity_name like 'USP%' and ref.referenced_entity_name NOT IN (r.ReferencedObject) 
    )

    SELECT distinct ReferencingObject, ReferencedObject,Stack FROM Refobjects 
	  union all
		   SELECT   '{uspname}' as ReferencingObject,
					ref.referenced_entity_name,
					0 as Stack
		   FROM sys.dm_sql_referenced_entities ('{uspname}', 'OBJECT') ref where referenced_entity_name like 'USP%'
    order by Stack