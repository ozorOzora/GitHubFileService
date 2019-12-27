using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OpenApiDocuments.Core.DAL
{
    /// <summary>
    /// Représente l'entrepot de données de l'entité T.
    /// </summary>
    /// <typeparam name="T">Type de l'entité.</typeparam>
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MongoDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe GenericRepository à l'aide d'un contexte.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recherche un élément qui correspond aux conditions définies par le prédicat spécifié et retourne la première occurrence.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <returns>Premier élément qui correspond aux conditions définies par le prédicat spécifié, s'il est trouvé ; sinon, la valeur par défaut pour le type T.</returns>
        public T Find(Expression<Func<T, bool>> match)
        {
            return _context.GetCollection<T>().AsQueryable().FirstOrDefault(match);
        }

        /// <summary>
        /// Recherche un élément qui correspond aux conditions définies par le prédicat spécifié et retourne la première occurrence.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <param name="includes">Délégué Predicate qui définit les liaisons des éléments à charger.</param>
        /// <returns>Premier élément qui correspond aux conditions définies par le prédicat spécifié, s'il est trouvé ; sinon, la valeur par défaut pour le type T.</returns>
        public T Find(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Récupère tous les éléments qui correspondent au filtre spécifié.
        /// </summary>
        /// <param name="filter">Représentant le filtre à appliquer.</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        public List<T> FindAll(FilterDefinition<T> filter)
        {
            return _context.GetCollection<T>().Find(filter).ToList();
        }

        /// <summary>
        /// Récupère tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à rechercher.</param>
        /// <param name="orderBy">Délégué Predicate qui définit l'ordonnancement des éléments à retourner.</param>
        /// <param name="skip">Nombre d'élément à ignorer</param>
        /// <param name="take">Nombre d'éléments à retourner</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        public List<T> FindAll(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            /// Applique le filtre principal
            IQueryable<T> query = _context.GetCollection<T>().AsQueryable().Where(match);

            /// Applique le tri
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            /// Applique la pagination
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }

        /// <summary>
        /// Récupère tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à rechercher.</param>
        /// <param name="includes">Délégué Predicate qui définit les liaisons des éléments à charger.</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        public List<T> FindAll(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Récupère tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à rechercher.</param>
        /// <param name="orderBy">Délégué Predicate qui définit l'ordonnancement des éléments à retourner.</param>
        /// <param name="skip">Nombre d'élément à ignorer</param>
        /// <param name="take">Nombre d'éléments à retourner</param>
        /// <param name="includes">Délégué Predicate qui définit les liaisons des éléments à charger.</param>
        /// <returns>Liste contenant tous les éléments qui correspondent aux conditions définies par le prédicat spécifié, si une correspondance est trouvée ; sinon, un IEnumerable vide.</returns>
        public List<T> FindAll(Expression<Func<T, bool>> match, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recherche si un élément qui correspond aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <returns>Booléen indiquant si au moins un élément correspond aux conditions définies par le prédicat spécifié.</returns>
        public bool Any(Expression<Func<T, bool>> match)
        {
            return _context.GetCollection<T>().AsQueryable().Any(match);
        }

        /// <summary>
        /// Retourne le nombre d'éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à rechercher.</param>
        /// <returns>Nombre d'éléments correspondants aux conditions définies par le prédicat spécifié.</returns>
        public int Count(Expression<Func<T, bool>> match)
        {
            return _context.GetCollection<T>().AsQueryable().Count(match);
        }

        /// <summary>
        /// Ajoute un objet à la source de données.
        /// </summary>
        /// <param name="entity">Objet à ajouter à la source de données.</param>
        public void Add(T entity)
        {
            _context.GetCollection<T>().InsertOne(entity);
        }

        /// <summary>
        /// Met à jour un objet de la source de données.
        /// </summary>
        /// <param name="entity">Objet à mettre à jour dans la source de données.</param>
        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Met à jour l'élément qui correspond aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions de l'élément à mettre à jour.</param>
        /// <param name="entity">Objet à mettre à jour dans la source de données.</param>
        public void Update(Expression<Func<T, bool>> match, T entity)
        {
            _context.GetCollection<T>().ReplaceOne(match, entity);
        }

        /// <summary>
        /// Met à jour tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à mettre à jour.</param>
        /// <param name="update">Délégué Predicate qui définit les modifications des éléments à mettre à jour.</param>
        public void Update(Expression<Func<T, bool>> match, Expression<Func<T, T>> expression)
        {
            //TODO : Translate Expression to BSONDocument
            throw new NotImplementedException();
        }

        /// <summary>
        /// Met à jour tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à mettre à jour.</param>
        public void Update(Expression<Func<T, T>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime la première occurrence d'un objet spécifique de la source de données.
        /// </summary>
        /// <param name="entity">Objet à supprimer de la source de données.</param>
        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Supprime tous les éléments qui correspondent aux conditions définies par le prédicat spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à supprimer.</param>
        public void Delete(Expression<Func<T, bool>> match)
        {
            _context.GetCollection<T>().DeleteMany(match);
        }

        /// <summary>
        /// Créé un Single-field index pour le champ spécifié.
        /// </summary>
        /// <param name="match">Délégué Predicate qui définit les conditions des éléments à supprimer.</param>
        public void CreateSingleFieldIndex(Expression<Func<T, object>> field)
        {
            var _key = Builders<T>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<T>(_key);
            _context.GetCollection<T>().Indexes.CreateOne(indexModel);
        }

        /// <summary>
        /// Créé un Single-field index pour le champ spécifié.
        /// </summary>
        /// <param name="field">Chaine de caractère décrivant le champ à indexer.</param>
        public void CreateSingleFieldIndex(string field)
        {
            var _key = Builders<T>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<T>(_key);
            _context.GetCollection<T>().Indexes.CreateOne(indexModel);
        }
    }
}
