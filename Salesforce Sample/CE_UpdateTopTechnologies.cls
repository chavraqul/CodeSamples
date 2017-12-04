/**************************************************
* Top Skills Updater 
* -------------------------------------------------
* Contributors: 
*   Raquel Chavez (raquel.chavez@hpe.com) 
* Last Modified: January 05, 2016
* -------------------------------------------------
* Updates member skills snapshop object 
* data for an activity in the following status/phase:
*   -Public, In Registration, In Assignment,
*    In Submission, In Review, Review Complete,
*    Accepted, Completed.
* The query excludes any activity with no members
* assigned.
* Data will be ordered by top 10 most used skills,
* order by number of members assigned in descending
* order then by skill name in ascending order.
**************************************************/


global class CE_UpdateTopTechnologies implements Schedulable
{
    global void execute(SchedulableContext SC) 
    {
        // Clean start of Member Skill Snapshot custom object
        List<CE_Member_Skill_Snapshot__c> oldSkills = [select id from CE_Member_Skill_Snapshot__c];
        delete oldSkills;

        // New Top Technologies list to add
        List<CE_Member_Skill_Snapshot__c> topTechnologiesList = new List<CE_Member_Skill_Snapshot__c>();
        
        // Query activity skills
        AggregateResult[] groupedResults =  [SELECT Skill__r.Name, 
                                             COUNT(CE_Activity__r.Name), 
                                             COUNT(CE_Activity__r.Assigned_Member__c) 
                                             FROM CE_ActivitySkills__c 
                                             WHERE CE_Activity__r.Status__c NOT IN ('Draft' , 'Canceled') 
                                             GROUP BY Skill__r.Name 
                                             HAVING COUNT(CE_Activity__r.Assigned_Member__c) > 0 
                                             ORDER BY COUNT(CE_Activity__r.Assigned_Member__c) DESC, Skill__r.Name ASC NULLS LAST
                                             LIMIT 10];
        // Add the results to a list                             
        for (AggregateResult ar : groupedResults)  {
            // Add to top Technologies List
            // Add Counts
            CE_Member_Skill_Snapshot__c topSkills = new CE_Member_Skill_Snapshot__c();
            topSkills.Name = (String)ar.get('Name');
            topSkills.Skill_Name__c = (String)ar.get('Name');
            topSkills.Member_Count__c = (Integer)ar.get('expr1');
            topSkills.Timestamp__c = DateTime.now();
            topTechnologiesList.add(topSkills);
            
        }
                
        // Upsert skills into Member Skill Snapshot custom object
        upsert topTechnologiesList Skill__r.Name ;
          
          
       // Clean start of Publisher Skill Snapshot custom object
        List<CE_Publisher_Skill_Snapshot__c> oldPublisherSkills = [select id from CE_Publisher_Skill_Snapshot__c];
        delete oldPublisherSkills;

        // New Publisher Skills Used list to add
        List<CE_Publisher_Skill_Snapshot__c> publisherTechnologiesList = new List<CE_Publisher_Skill_Snapshot__c>();
        
        // Query activity skills
         AggregateResult[] groupedResultsPublisher =  [SELECT Skill__r.Name, 
																COUNT(CE_Activity__r.Name), 
																CE_Activity__r.Publisher__c 
																FROM CE_ActivitySkills__c 
																WHERE CE_Activity__r.Status__c NOT IN ('Draft', 'Canceled') 
																	AND CE_Activity__r.Assigned_Member__c != null 
																GROUP BY CE_Activity__r.Publisher__c, Skill__r.Name 
																ORDER BY COUNT(CE_Activity__r.Name) DESC, Skill__r.Name ASC NULLS LAST ];
        // Add the results to a list                             
        for (AggregateResult publist : groupedResultsPublisher)  {
            // Add to publisher skill list
            CE_Publisher_Skill_Snapshot__c publisherSkills = new CE_Publisher_Skill_Snapshot__c();
            publisherSkills.Name = (String)publist.get('Name');
            publisherSkills.Skill_Name__c = (String)publist .get('Name');
            publisherSkills.Activity_Count__c = (Integer)publist.get('expr0');
            publisherSkills.Publisher__c = (ID)publist .get('Publisher__c');
            publisherSkills.Timestamp__c = DateTime.now();
            publisherTechnologiesList.add(publisherSkills);
                        
        }
          
        // Upsert skills into Publisher Skill Snapshot custom object
        insert publisherTechnologiesList;
          
    }

}
