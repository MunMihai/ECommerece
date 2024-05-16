# ECommerece
An e-commerce API that utilizes nine design patterns: FACTORY METHOD, PROTOTYPE, BUILDER, PROXY, FACADE, ADAPTER, CHAIN OF RESPONSABILITY, MEDIATOR, COMMAND

Factory Method: Ecommerce.BL>PaymentFactory & Ecommerce.Core>Entities>PaymentDetails
     - utilizat pentru a gestiona diferite metode de plată, în funcție de necesitate
creând PaymentDetails necesar

Prototype: Ecommerce.BL>Services>OrderPrototype.cs & OrderService.cs
    - este utilizat pentru clonarea unui Oreder deja existent

Builder: Ecommerce.BL>Services>ShoppingCartBuilder.cs & CartService.cs
    - construieste și adaugă produsele după necesitate în coș 

Proxy: Ecommerce.BL>Services>OrderService.cs & Ecommerce.Api>Controllers>OrderController.cs
    - adăuga înregistrări de loguri pentru operațiile efectuate de OrderService

Facade: Ecommerce.BL>DbServices>ProductRepository.cs & Ecommerce.Api>Controllers>ProductsController.cs
    - întreaga logică de validare, mapare(adaptare), si interacțiunea cu baza de date este ascunsă în ProductRepository. Controllerul utilizează o interfață     
   simplificată pentru metodele de gestionare a produselor.

Adapter: Ecommerce.BL>Adapter>ProductAdapter.cs & Service>OrderService.cs
