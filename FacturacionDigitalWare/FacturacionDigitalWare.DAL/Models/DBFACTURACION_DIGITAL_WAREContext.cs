using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FacturacionDigitalWare.DAL.Models
{
    public partial class DBFACTURACION_DIGITAL_WAREContext : DbContext
    {
        public DBFACTURACION_DIGITAL_WAREContext()
        {
        }

        public DBFACTURACION_DIGITAL_WAREContext(DbContextOptions<DBFACTURACION_DIGITAL_WAREContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ClienteCorreo> ClienteCorreos { get; set; }
        public virtual DbSet<ClienteTelefono> ClienteTelefonos { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturaProducto> FacturaProductos { get; set; }
        public virtual DbSet<Inventario> Inventarios { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<TipoCorreo> TipoCorreos { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }
        public virtual DbSet<TipoTelefono> TipoTelefonos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=DBFACTURACION_DIGITAL_WARE;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.CiuIdCiudad)
                    .HasName("PK__ciudad__6CD790897A649D7C");

                entity.ToTable("ciudad");

                entity.Property(e => e.CiuIdCiudad)
                    .ValueGeneratedNever()
                    .HasColumnName("ciu_id_ciudad");

                entity.Property(e => e.CiuActivo).HasColumnName("ciu_activo");

                entity.Property(e => e.CiuIdDepartamento).HasColumnName("ciu_id_departamento");

                entity.Property(e => e.CiuNombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ciu_nombre");

                entity.HasOne(d => d.CiuIdDepartamentoNavigation)
                    .WithMany(p => p.Ciudads)
                    .HasForeignKey(d => d.CiuIdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ciudad__departamento");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CliIdCliente)
                    .HasName("PK__cliente__8D4F3F697306EAC8");

                entity.ToTable("cliente");

                entity.Property(e => e.CliIdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("cli_id_cliente");

                entity.Property(e => e.CliActivo).HasColumnName("cli_activo");

                entity.Property(e => e.CliApellidos)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_apellidos");

                entity.Property(e => e.CliDireccion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_direccion");

                entity.Property(e => e.CliFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("cli_fecha_creacion");

                entity.Property(e => e.CliFechaNacimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("cli_fecha_nacimiento");

                entity.Property(e => e.CliIdCiudadNacimiento).HasColumnName("cli_id_ciudad_nacimiento");

                entity.Property(e => e.CliIdCiudadResidencia).HasColumnName("cli_id_ciudad_residencia");

                entity.Property(e => e.CliIdTipoIdentificacion).HasColumnName("cli_id_tipo_identificacion");

                entity.Property(e => e.CliIdentificacion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cli_identificacion");

                entity.Property(e => e.CliNombres)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cli_nombres");

                entity.HasOne(d => d.CliIdCiudadNacimientoNavigation)
                    .WithMany(p => p.ClienteCliIdCiudadNacimientoNavigations)
                    .HasForeignKey(d => d.CliIdCiudadNacimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente__ciudad__nacimiento");

                entity.HasOne(d => d.CliIdCiudadResidenciaNavigation)
                    .WithMany(p => p.ClienteCliIdCiudadResidenciaNavigations)
                    .HasForeignKey(d => d.CliIdCiudadResidencia)
                    .HasConstraintName("FK_cliente__ciudad__residencia");

                entity.HasOne(d => d.CliIdTipoIdentificacionNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.CliIdTipoIdentificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente__tipo_identificacion");
            });

            modelBuilder.Entity<ClienteCorreo>(entity =>
            {
                entity.HasKey(e => new { e.CcoIdCliente, e.CcoIdTipoCorreo })
                    .HasName("PK__cliente___7D949567C2386516");

                entity.ToTable("cliente_correo");

                entity.Property(e => e.CcoIdCliente).HasColumnName("cco_id_cliente");

                entity.Property(e => e.CcoIdTipoCorreo).HasColumnName("cco_id_tipo_correo");

                entity.Property(e => e.CcoActivo).HasColumnName("cco_activo");

                entity.Property(e => e.CcoCorreo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cco_correo");

                entity.HasOne(d => d.CcoIdClienteNavigation)
                    .WithMany(p => p.ClienteCorreos)
                    .HasForeignKey(d => d.CcoIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_correo__cliente");

                entity.HasOne(d => d.CcoIdTipoCorreoNavigation)
                    .WithMany(p => p.ClienteCorreos)
                    .HasForeignKey(d => d.CcoIdTipoCorreo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_correo__tipo_correo");
            });

            modelBuilder.Entity<ClienteTelefono>(entity =>
            {
                entity.HasKey(e => new { e.CteIdCliente, e.CteIdTipoTelefono })
                    .HasName("PK__cliente___DA62A827877A0A6E");

                entity.ToTable("cliente_telefono");

                entity.Property(e => e.CteIdCliente).HasColumnName("cte_id_cliente");

                entity.Property(e => e.CteIdTipoTelefono).HasColumnName("cte_id_tipo_telefono");

                entity.Property(e => e.CteTelefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cte_telefono");

                entity.HasOne(d => d.CteIdClienteNavigation)
                    .WithMany(p => p.ClienteTelefonos)
                    .HasForeignKey(d => d.CteIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_telefono__cliente");

                entity.HasOne(d => d.CteIdTipoTelefonoNavigation)
                    .WithMany(p => p.ClienteTelefonos)
                    .HasForeignKey(d => d.CteIdTipoTelefono)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_telefono__tipo_telefono");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.DepIdDepartamento)
                    .HasName("PK__departam__33748023E2871919");

                entity.ToTable("departamento");

                entity.Property(e => e.DepIdDepartamento)
                    .ValueGeneratedNever()
                    .HasColumnName("dep_id_departamento");

                entity.Property(e => e.DepActivo).HasColumnName("dep_activo");

                entity.Property(e => e.DepIdPais).HasColumnName("dep_id_pais");

                entity.Property(e => e.DepNombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("dep_nombre");

                entity.HasOne(d => d.DepIdPaisNavigation)
                    .WithMany(p => p.Departamentos)
                    .HasForeignKey(d => d.DepIdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_departamento__pais");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.FacIdFactura)
                    .HasName("PK__factura__7EE1768925396D0A");

                entity.ToTable("factura");

                entity.Property(e => e.FacIdFactura)
                    .ValueGeneratedNever()
                    .HasColumnName("fac_id_factura");

                entity.Property(e => e.FacFechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fac_fecha_registro");

                entity.Property(e => e.FacIdCliente).HasColumnName("fac_id_cliente");

                entity.Property(e => e.FacValorTotal)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("fac_valor_total");

                entity.HasOne(d => d.FacIdClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.FacIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura__cliente");
            });

            modelBuilder.Entity<FacturaProducto>(entity =>
            {
                entity.HasKey(e => new { e.FprIdCompra, e.FprIdProducto })
                    .HasName("PK__factura_p__10B0127204B19213");

                entity.ToTable("factura_producto");

                entity.Property(e => e.FprIdCompra).HasColumnName("fpr_id_compra");

                entity.Property(e => e.FprIdProducto).HasColumnName("fpr_id_producto");

                entity.Property(e => e.FprCantidad).HasColumnName("fpr_cantidad");

                entity.Property(e => e.FprValor)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("fpr_valor");

                entity.HasOne(d => d.FprIdCompraNavigation)
                    .WithMany(p => p.FacturaProductos)
                    .HasForeignKey(d => d.FprIdCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_producto__factura");

                entity.HasOne(d => d.FprIdProductoNavigation)
                    .WithMany(p => p.FacturaProductos)
                    .HasForeignKey(d => d.FprIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_producto__producto");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.InvIdInventario)
                    .HasName("PK__inventar__A6B40AF9DC6E71CB");

                entity.ToTable("inventario");

                entity.Property(e => e.InvIdInventario)
                    .ValueGeneratedNever()
                    .HasColumnName("inv_id_inventario");

                entity.Property(e => e.InvCantidad).HasColumnName("inv_cantidad");

                entity.Property(e => e.InvIdProducto).HasColumnName("inv_id_producto");

                entity.HasOne(d => d.InvIdProductoNavigation)
                    .WithMany(p => p.Inventarios)
                    .HasForeignKey(d => d.InvIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_inventario__producto");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.PaiIdPais)
                    .HasName("PK__pais__655B5337D73EBE7F");

                entity.ToTable("pais");

                entity.Property(e => e.PaiIdPais)
                    .ValueGeneratedNever()
                    .HasColumnName("pai_id_pais");

                entity.Property(e => e.PaiActivo).HasColumnName("pai_activo");

                entity.Property(e => e.PaiNombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pai_nombre");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.ProIdProducto)
                    .HasName("PK__producto__D516B6A13BF05FB3");

                entity.ToTable("producto");

                entity.Property(e => e.ProIdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("pro_id_producto");

                entity.Property(e => e.ProActivo).HasColumnName("pro_activo");

                entity.Property(e => e.ProDescripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pro_descripcion");

                entity.Property(e => e.ProFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("pro_fecha_creacion");

                entity.Property(e => e.ProNombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pro_nombre");

                entity.Property(e => e.ProPrecio)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("pro_precio");
            });

            modelBuilder.Entity<TipoCorreo>(entity =>
            {
                entity.HasKey(e => e.TcoIdTipoCorreo)
                    .HasName("PK__tipo_cor__7E6B40181B83A071");

                entity.ToTable("tipo_correo");

                entity.Property(e => e.TcoIdTipoCorreo)
                    .ValueGeneratedNever()
                    .HasColumnName("tco_id_tipo_correo");

                entity.Property(e => e.TcoActivo).HasColumnName("tco_activo");

                entity.Property(e => e.TcoDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tco_descripcion");
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.HasKey(e => e.TidIdTipoIdentificacion)
                    .HasName("PK__tipo_ide__C1A409E56BDFDB26");

                entity.ToTable("tipo_identificacion");

                entity.Property(e => e.TidIdTipoIdentificacion)
                    .ValueGeneratedNever()
                    .HasColumnName("tid_id_tipo_identificacion");

                entity.Property(e => e.TidActivo).HasColumnName("tid_activo");

                entity.Property(e => e.TidDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tid_descripcion");
            });

            modelBuilder.Entity<TipoTelefono>(entity =>
            {
                entity.HasKey(e => e.TteIdTipoTelefono)
                    .HasName("PK__tipo_tel__DC7B10B22B209F14");

                entity.ToTable("tipo_telefono");

                entity.Property(e => e.TteIdTipoTelefono)
                    .ValueGeneratedNever()
                    .HasColumnName("tte_id_tipo_telefono");

                entity.Property(e => e.TteActivo).HasColumnName("tte_activo");

                entity.Property(e => e.TteDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tte_descripcion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
