<template>
	<div class="container">

    <el-table :data="tableData" style="width: 100%">
      <el-table-column prop="date" label="Date" width="180" />
      <el-table-column prop="name" label="Name" width="180" />
      <el-table-column prop="address" label="Address" />
    </el-table>

    

    
  </div>

</template>

<script lang="ts" setup>
 const tableData = [
      {
        date: '2016-05-03',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
      },
      {
        date: '2016-05-02',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
      },
      {
        date: '2016-05-04',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
      },
      {
        date: '2016-05-01',
        name: 'Tom',
        address: 'No. 189, Grove St, Los Angeles',
      },
    ]
</script>



<stype lang="scss" scoped>
  .container{
    margin: 100px auto;
    width:50%
  }
  .table{
    margin:10px 0;
  }

  .input-with-select{
    width: 380px;
    margin-right: 20px;

  }
</stype>

