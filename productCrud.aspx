<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productCrud.aspx.cs" Inherits="project2.product_crud.productCrud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Product Crud Operation</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css">
    <style>
        .taha {
            width: 100%;
            margin-left: -300px;
        }
    </style>
</head>

<body class="bg-gray-100">
    <form id="formAddProduct" runat="server" class="max-w-xl mx-auto mt-8">
        <div class="mb-4">
            <label for="txtProductName" class="block text-sm font-medium text-gray-700">Product Name</label>
            <asp:TextBox ID="txtProductName" runat="server" CssClass="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"></asp:TextBox>
        </div>
        <div class="mb-4">
            <label for="txtQuantity" class="block text-sm font-medium text-gray-700">Quantity</label>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"></asp:TextBox>
        </div>
        <div class="mb-4">
            <label for="txtCategory" class="block text-sm font-medium text-gray-700">Category</label>
            <asp:TextBox ID="txtCategory" runat="server" CssClass="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"></asp:TextBox>
        </div>
        <div class="mb-4">
            <label for="txtPrice" class="block text-sm font-medium text-gray-700">Price</label>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"></asp:TextBox>
        </div>
        <div class="mb-4">
            <label for="fileImage" class="block text-sm font-medium text-gray-700">Image Upload</label>
            <asp:FileUpload ID="fileImage" runat="server" CssClass="mt-1 block w-full" />
        </div>
        <div class="mb-4">
            <label for="txtDescription" class="block text-sm font-medium text-gray-700">Description</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:border-indigo-500 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"></asp:TextBox>
        </div>
        <div class="flex justify-end mb-4">
            <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" CssClass="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 focus:outline-none focus:bg-blue-600" OnClick="btnAddProduct_Click" />
        </div>
        <div>

            <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="w-full taha" OnRowDeleting="gvProducts_RowDeleting" OnRowEditing="gvProducts_RowEditing">

                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="Product ID" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2" />
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2" />
                    <asp:BoundField DataField="Price" HeaderText="Price" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2" />
                    <asp:TemplateField HeaderText="Image" ItemStyle-Width="150px" ItemStyle-CssClass="px-4 py-2 text-center" HeaderStyle-CssClass="px-4 py-2 text-left">
                        <ItemTemplate>
                            <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' AlternateText='<%# Eval("ProductName") %>' CssClass="max-w-full h-auto mx-auto" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2" />
                    <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="text-center px-4 py-2" HeaderStyle-CssClass="text-left px-4 py-2">
                        <ItemTemplate>
                            <div class="flex justify-center">
                                <asp:Button runat="server" Text="Edit" CssClass="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded mr-2" CommandName="Edit" CommandArgument='<%# Eval("ProductID") %>' OnClick="edit" />
                                <asp:Button runat="server" Text="Delete" CssClass="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded" CommandName="Delete" CommandArgument='<%# Eval("ProductID") %>' OnClick="delete" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>



        </div>
    </form>

</body>
</html>
