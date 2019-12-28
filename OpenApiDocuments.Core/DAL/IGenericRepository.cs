using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OpenApiDocuments.Core.DAL
{
    /// <summary>
    /// Définit des méthodes pour manipuler les entités de type T.
    /// </summary>
    /// <typeparam name="T">Type de l'entité.</typeparam>
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Recherche un élément qui correspond aux conditions définies par le prédicat spécifié et retourne la première occurrence.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <returns>Premier élément qui correspond aux conditions définies par le prédicat spécifié, s'il est trouvé ; sinon, la valeur par défaut pour le type T.</returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// Recherche un élément qui correspond aux conditions définies par le prédicat spécifié et retourne la première occurrence.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <param name="includes">Délégué Predicate qui définit les liaisons des éléments à charger.</param>
        /// <returns>Premier élément qui correspond aux conditions définies par le prédicat spécifié, s'il est trouvé ; sinon, la valeur par défaut pour le type T.</returns>
        T Find(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// Récupère tous les éléments qui correspondent au filtre spécifié.
        /// </summary>
        /// <param name="filter">Représentant le filtre à appliquer.</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        List<T> FindAll(FilterDefinition<T> filter);

        /// <summary>
        /// Récupère tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à rechercher.</param>
        /// <param name="orderBy">Délégué Predicate qui définit l'ordonnancement des éléments à retourner.</param>
        /// <param name="skip">Nombre d'élément à ignorer</param>
        /// <param name="take">Nombre d'éléments à retourner</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        List<T> FindAll(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null);

        /// <summary>
        /// Récupère tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à rechercher.</param>
        /// <param name="includes">Délégué Predicate qui définit les liaisons des éléments à charger.</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        List<T> FindAll(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Récupère tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à rechercher.</param>
        /// <param name="orderBy">Délégué Predicate qui définit l'ordonnancement des éléments à retourner.</param>
        /// <param name="skip">Nombre d'élément à ignorer</param>
        /// <param name="take">Nombre d'éléments à retourner</param>
        /// <param name="includes">Délégué Predicate qui définit les liaisons des éléments à charger.</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        List<T> FindAll(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Recherche si un élément qui correspond aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <returns>Booléen indiquant si au moins un élément correspond aux conditions définies par le prédicat spécifié.</returns>
        bool Any(Expression<Func<T, bool>> match);

        /// <summary>
        /// Retourne le nombre d'éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <returns>Nombre d'éléments correspondants aux conditions définies par le prédicat spécifié.</returns>
        int Count(Expression<Func<T, bool>> match);

        /// <summary>
        /// Ajoute un objet à la source de données.
        /// </summary>
        /// <param name="entity">Objet à ajouter à la source de données.</param>
        void Add(T entity);

        /// <summary>
        /// Met à jour un objet de la source de données.
        /// </summary>
        /// <param name="entity">Objet à mettre à jour dans la source de données.</param>
        void Update(T entity);

        /// <summary>
        /// Met à jour l'élément qui correspond aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à mettre à jour.</param>
        /// <param name="entity">Objet à mettre à jour dans la source de données.</param>
        void Update(Expression<Func<T, bool>> match, T entity);

        /// <summary>
        /// Met à jour tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à mettre à jour.</param>
        /// <param name="update">Délégué Predicate qui définit les modifications des éléments à mettre à jour.</param>
        void Update(Expression<Func<T, bool>> match, Expression<Func<T, T>> update);

        /// <summary>
        /// Met à jour tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à mettre à jour.</param>
        void Update(Expression<Func<T, T>> update);

        /// <summary>
        /// Supprime la première occurrence d'un objet spécifique de la source de données.
        /// </summary>
        /// <param name="entity">Objet à supprimer de la source de données.</param>
        void Remove(T entity);

        /// <summary>
        /// Supprime tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à supprimer.</param>
        void Delete(Expression<Func<T, bool>> match);

        /// <summary>
        /// Enregistre un fichier depuis un bytes array 
        /// </summary>
        /// <param name="fileContent">Contenu du fichier représenté sous la forme d'un tableau d'octets</param>
        /// <param name="metadata">Métadonnées pour le fichier à enregistrer</param>
        void UploadFile(byte[] fileContent, T metadata);

        /// <summary>
        /// Créé un Single-field index pour le champ spécifié.
        /// </summary>
        /// <param name="field">Délégué Predicate qui définit les conditions des éléments à supprimer.</param>
        void CreateSingleFieldIndex(Expression<Func<T, object>> field);

        /// <summary>
        /// Créé un Single-field index pour le champ spécifié.
        /// </summary>
        /// <param name="field">Chaine de caractère décrivant le champ à indexer.</param>
        void CreateSingleFieldIndex(string field);
    }
}
