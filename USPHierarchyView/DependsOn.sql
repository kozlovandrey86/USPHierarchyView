WITH Refobjects 
            AS 
            (
                SELECT
                o.name AS ReferencingObject,
                sed.referenced_entity_name AS ReferencedObject,
		        0 as Stack
                FROM
                sys.sql_expression_dependencies sed
                INNER JOIN
                sys.objects o ON sed.referencing_id = o.[object_id]
                WHERE
                sed.referenced_entity_name = '{uspname}'
              UNION ALL 

                SELECT
                    o.name AS ReferencingObject,
                    Refobjects.ReferencingObject AS ReferencedObject,
			        Refobjects.Stack + 1
                FROM
                    sys.sql_expression_dependencies sed
                    INNER JOIN
                    sys.objects o ON sed.referencing_id = o.[object_id]
                    INNER JOIN Refobjects ON sed.referenced_entity_name = Refobjects.ReferencingObject 
			
            )
            SELECT distinct ReferencingObject, ReferencedObject, Stack FROM Refobjects order by Stack