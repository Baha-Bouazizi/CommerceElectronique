# 🛒 Application de Commerce Électronique  
<p align="center">
  <img src="public/images/institut-logo.png" alt="Logo de l'Institut" width="150"/>
  &nbsp;&nbsp;&nbsp;
  <img src="public/images/application-logo.png" alt="Logo de l'Application" width="150"/>
</p>

## Introduction  

Bienvenue dans l'Application de Commerce Électronique, une plateforme intuitive conçue pour améliorer l'expérience d'achat en ligne. Développée dans le cadre d'un projet académique, cette application propose une gestion complète des produits, des commandes et des utilisateurs, tout en intégrant un système de paiement sécurisé via Stripe.  

## Aperçu des Fonctionnalités  

### 🌟 Fonctionnalités Principales  

- **Gestion des Produits** : Parcourez et gérez les produits selon vos préférences.  
- **Panier et Commandes** : Ajoutez des produits à votre panier et finalisez vos achats.  
- **Paiement Sécurisé avec Stripe** : Effectuez vos transactions en toute sécurité.  
- **Historique des Commandes** : Consultez vos commandes passées après connexion.  
- **Administration** : Gérez les produits, les catégories et les commandes via un tableau de bord dédié.  

## Fonctionnalités Détaillées  

### 🛒 Gestion des Produits  

- **Recherche Avancée** : Trouvez des produits par nom ou par prix.  
- **Catégories** : Explorez les produits organisés en catégories distinctes.  
- **Affichage Dynamique** : Ajoutez ou retirez des produits de votre panier en temps réel.  

### 📦 Commandes et Historique  

- **Suivi des Commandes** : Accédez à l'historique de vos commandes après authentification.  
- **Notifications** : Recevez des emails de confirmation après validation des commandes.  

### 💳 Paiement Sécurisé  

- **Intégration Stripe** : Configurez vos clés API dans le fichier de configuration :  
  ```json  
  "Stripe": {  
    "PublishableKey": "VOTRE_PUBLISHABLE_KEY",  
    "SecretKey": "VOTRE_SECRET_KEY"  
  }  
🔒 Administration  
Gestion des Produits : Ajoutez, modifiez ou supprimez des produits facilement.  
Gestion des Catégories : Créez ou supprimez des catégories pour une meilleure organisation.  
Commandes : Validez et gérez les commandes des utilisateurs.  

🛠 Instructions d'Utilisation  
Explorez les fonctionnalités principales de l'application :  

- **Produits** : Recherchez, ajoutez ou gérez les produits.  
- **Panier** : Gérez les produits ajoutés à votre panier.  
- **Paiement** : Finalisez vos commandes via Stripe.  

🚀 Guide d'Installation  
Pour installer et exécuter le projet en local, suivez ces étapes :  

1. **Clonez le Dépôt** :  
   ```bash  
   git clone https://github.com/Baha-Bouazizi/Ecommerce-Application  
