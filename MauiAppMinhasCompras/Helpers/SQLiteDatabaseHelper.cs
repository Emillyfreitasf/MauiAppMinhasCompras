﻿using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<int> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=?, Categoria=? WHERE Id=?";

            return _conn.ExecuteAsync(sql, p.Descricao, p.Quantidade, p.Preco, p.Categoria, p.Id);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%' || ? || '%'";
            return _conn.QueryAsync<Produto>(sql, q);
        }

        public Task<List<Produto>> GetByCategoria(string categoria)
        {
            string sql = "SELECT * FROM Produto WHERE Categoria = ?";
            return _conn.QueryAsync<Produto>(sql, categoria);
        }
        public Task<List<Produto>> SearchByCategoryOrDescription(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%' || ? || '%' OR Categoria LIKE '%' || ? || '%'";
            return _conn.QueryAsync<Produto>(sql, q, q);
        }

    }
}

