# FinalProjectOfUnittest
## Applied Features
1. Paging(user can change page size).<br/>
2. Serching by Title, submitter name, Assigned developer name.<br/>
3. Sort by newest, ticket type, ticket priority, ticket status.<br/>
4. Upload and Download attached files.<br/>
5. Notice( A.When developer is assigned, devpeloper get notice.  B.when ticket completed, project manager get notice ) 
6. User can log in as Guest without Sign up. Guset User has all roles.
7. Success unit test of Get(Func<T,bool> func) function in the RoleBLLTest. 
## Changes compared to existing requirements
1. Guest user has all roles for advanture all functions of proejct.
2. TicketLogItem class(image of previous ticket) which has one to one relation ship with TicketHistory.
