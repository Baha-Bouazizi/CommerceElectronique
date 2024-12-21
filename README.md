# Commerce Électronique

Ce projet est une plateforme de commerce électronique développée dans un cadre académique. Elle permet aux utilisateurs de parcourir des produits par catégories, de les ajouter à leur panier, de finaliser leurs achats via un paiement sécurisé, et d'accéder à l'historique de leurs commandes. Les administrateurs disposent de fonctionnalités avancées pour gérer les produits, catégories et commandes.

---

## Fonctionnalités

### Utilisateurs
- **Gestion du Panier :** Ajouter, modifier ou supprimer des produits dans le panier.  
- **Commandes :** Passer des commandes et effectuer des paiements sécurisés via **Stripe**.  
- **Historique des Commandes :** Consulter les commandes passées après connexion.  
- **Recherche et Filtrage :** Trouver des produits par nom, catégorie ou prix.  

### Administrateurs
- **Gestion des Produits :** Ajouter, modifier ou supprimer des produits.  
- **Gestion des Catégories :** Gérer les catégories de produits (création, modification, suppression).  
- **Gestion des Commandes :** Approuver ou rejeter les commandes des utilisateurs. Un email de confirmation est envoyé automatiquement après l’approbation.  

---

## Technologies utilisées
- **Backend :** ASP.NET MVC  
- **Base de données :** SQL Server  
- **Frontend :** HTML, CSS, JavaScript  
- **Paiement :** Intégration avec **Stripe** pour des paiements sécurisés  
- **Emailing :** Envoi d'emails via une API SMTP  

---

## Installation et exécution

### Étapes détaillées pour configurer et exécuter le projet :

1. **Cloner le dépôt GitHub**  
   Clonez le projet sur votre machine locale :  
   ```bash
   git clone https://github.com/Baha-Bouazizi/CommerceElectronique.git
   cd CommerceElectronique
