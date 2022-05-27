# FinalProjectOfUnittest( Ticket(Request) system
- this proeject is making Ticket(request) system.
![image](https://user-images.githubusercontent.com/95237930/170739657-cb7e748f-0694-40e1-87d4-1052fbd62f99.png)

## Applied Features
1. Paging(user can change page size).<br/>
2. Serching by Title, submitter name, Assigned developer name.<br/>
3. Filter by Created Date,Ticket Type,Ticket Status,Ticket Priority<br/>
4. Sort by newest, ticket type, ticket priority, ticket status.<br/>
5. Upload and Download attached files.<br/>
6. Notice( A.When developer is assigned, devpeloper get notice.  B.when ticket completed, project manager get notice ) 
7. User can log in as Guest without Sign up. Guset User has all roles.
8. Success unit test of Get(Func<T,bool> func) function in the RoleBLLTest. 
## Changes compared to existing requirements
1. Guest user has all roles for advanture all functions of proejct.
2. TicketLogItem class(image of previous ticket) which has one to one relation ship with TicketHistory.

