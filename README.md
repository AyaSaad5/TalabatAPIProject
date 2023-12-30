Welcome to the Talabat API project! 
This project aims to provide developers with a seamless way to integrate Talabat's services into their applications. 
Talabat is a leading online food delivery service, and this API allows you to access various features 
to enhance the food ordering experience.
this project follows Onion architecture layers.

=> In this project we apply:

- User Authentcation and   Authrization
- Payment integration with Stripe
- Error Handling
- Exception Middleware to handle the errors globally and to facilitate it to Front-End side
- Logging 
- ⁠AutoMapper which is facilitated mapping between entities and DTOs.
and End Points for Products, Orders ,Basket and User Controllers. 
we enhance " refactor " project using UnitOfWork and 
to light the headache of memory , we apply Caching.

We used Redis as our distributed db for caching to enhance the performance of certain operations
